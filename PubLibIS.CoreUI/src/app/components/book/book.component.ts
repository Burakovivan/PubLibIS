import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { BookService } from '../../services/book.service';
import { Book } from '../../models/book';
import { resetFakeAsyncZone, fakeAsync } from '@angular/core/testing';
import { SelectList } from '../../models/selectList';
import * as $ from "jquery";

@Component({
    templateUrl: './book.component.html',
    providers: [BookService],
    styleUrls: ['../../styles/common.css']
})
export class BookComponent implements OnInit {
    book: Book = new Book();   // изменяемый товар
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
      this.dataService.getBookList().subscribe((books: Book[]) => this.books = books);
        this.loaded = true;
    }

    // сохранение данных
    saveBook() {
        if (this.book.id == null || this.book.id == -1) {
            let newBook: Book = new Book();
            this.dataService.createBook(this.book).subscribe(book => newBook = book)
            this.loadBooks();
        } else {
            this.dataService.updateBook(this.book).subscribe(data =>
                this.loadBooks());
        }
        this.cancelBook();
    }


    editBook(a: Book) {
        this.book = a;
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
