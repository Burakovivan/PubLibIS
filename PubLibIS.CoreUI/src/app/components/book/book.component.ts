import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { BookService } from '../../services/book.service';
import { Book } from '../../models/book';
import { resetFakeAsyncZone, fakeAsync } from '@angular/core/testing';
import { SelectList } from '../../models/selectList';
import * as $ from "jquery";
import { Author } from '../../models/author';
import { forEach } from '@angular/router/src/utils/collection';

@Component({
  templateUrl: './book.component.html',
  providers: [BookService],
  styleUrls: ['../../styles/common.css']
})
export class BookComponent implements OnInit {
  book: Book = new Book();   // изменяемый товар
  authorList: { id: number, itemName: string }[] = new Array<{ id: number, itemName: string }>();
  selectedAuthorList: { id: number, itemName: string }[] = new Array<{ id: number, itemName: string }>();
  dropdownSettings = {
  singleSelection: false,
  text: "Select Author",
  selectAllText: 'Select All',
  unSelectAllText: 'UnSelect All',
  enableSearchFilter: true,
}; 
  books: Book[];                // массив товаров
  loaded: boolean = true;
  jsonBackUpText: string;

  constructor(private dataService: BookService) {
  }

  ngOnInit() {

    this.loadBooks();    // загрузка данных при старте компонента  
  }
  // получаем данные через сервис
  loadBooks() {
    this.loaded = false;
    this.dataService.getBookList().subscribe((books: Book[]) => { this.books = books; console.log(this.books); });
    
    this.loaded = true;
  }

  // сохранение данных
  saveBook() {
    this.book.authors = this.selectedAuthorList.map(a => new Author(a.id));
    console.log(this.book);
    console.log(this.selectedAuthorList);
    if (this.book.id == null || this.book.id == -1) {
      this.dataService.createBook(this.book).subscribe((book: Book) => this.books.push(book))
    } else {
      this.dataService.updateBook(this.book).subscribe(() =>
        this.loadBooks());
    }
    this.cancelBook();
  }

  loadAuthorSelecList() {
    this.authorList = new Array<{ id: number, itemName: string }>();
    this.selectedAuthorList = new Array<{ id: number, itemName: string }>();
    this.dataService.getAuthorListByBook(this.book.id).subscribe((authorSelectList: SelectList) => {
      for (var i = 0; i < authorSelectList.items.length; i++) {
        var item = authorSelectList.items[i];
        if (item.selected) {
          this.selectedAuthorList.push({ id: item.value, itemName: item.text });
        }
        this.authorList.push({ id: item.value, itemName: item.text });

      }

    });
  }
  editBook(a: Book) {
    this.book = a;
    this.loadAuthorSelecList();
  }


  cancelBook() {
    this.book = new Book();
  }

  deleteBook(p: Book) {
    this.dataService.deleteBook(p.id as number)
      .subscribe(data => this.loadBooks());
  }

  createBook() {
    this.book = new Book();
    this.book.id = -1;
    this.loadAuthorSelecList();
  }
  getJson() {
    var ids: string[] = $("input:checked[name=backup]").toArray().map((e) => e.id);
    console.log(this.dataService.getJson(ids));
    console.log(ids);
  }
  setJson() {
    console.log(this.jsonBackUpText);
    this.dataService.setJson(this.jsonBackUpText);
  }
  setFile(files: FileList) {
    let blob: Blob = files.item(0).slice(0, files.item(0).size, files.item(0).type);
    let reader = new FileReader();
    reader.addEventListener("load", () => {
      this.jsonBackUpText = reader.result;
    }, false);
    reader.readAsText(blob, "");
  }
}
