import { ActivatedRoute } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { BookService } from '../book.service';
import { PublishedBook } from '../shared/published-book.model';
import { resetFakeAsyncZone, fakeAsync } from '@angular/core/testing';
import { SelectList } from '../../shared/select-list.model';
import { Book } from '../shared/book.model';

@Component({
  templateUrl: './published-book.component.html',
  providers: [BookService],
  styleUrls: []
})
export class PublishedBookComponent implements OnInit {

  publishedBook: PublishedBook;
  publishedBookList: PublishedBook[];
  book_id: number;
  book: Book;
  loaded: boolean = false;
  publishigHouseSelectList: SelectList;

  constructor(private dataService: BookService, private activatedRoute: ActivatedRoute) {
    this.book_id = activatedRoute.snapshot.params['id'];
    this.dataService.getBook(this.book_id).subscribe(b => this.book = b);
  }

  ngOnInit() {
    this.loadPublishedBooks();    // загрузка данных при старте компонента  
  }

  // получаем данные через сервис
  loadPublishedBooks() {
    this.loaded = false;
    this.dataService.getPublicationList(this.book_id).subscribe((publishedBooks: PublishedBook[]) => this.publishedBookList = publishedBooks);
    this.loaded = true;
  }

  // сохранение данных
  savePublishedBook() {
    if (this.publishedBook.id == null || this.publishedBook.id == -1) {
      this.publishedBook.book_Id = this.book_id;
      this.dataService.createPublication(this.publishedBook).subscribe(publishedBook => this.loadPublishedBooks());
    } else {

      this.dataService.updatePublication(this.publishedBook).subscribe(data =>
        this.loadPublishedBooks());
    }
    this.cancelPublishedBook();
  }


  editPublishedBook(a: PublishedBook) {
    this.dataService.getPublishingHouseSelectList(a.id as number).subscribe((phList: SelectList) => this.publishigHouseSelectList = phList);
    this.publishedBook = a;
  }


  cancelPublishedBook() {
    this.publishedBook = new PublishedBook();
  }

  deletePublishedBook(p: PublishedBook) {
    this.dataService.deletePublication(p.id as number)
      .subscribe(data => this.loadPublishedBooks());
  }


  createPublishedBook() {
    this.dataService.getPublishingHouseSelectList(this.book_id).subscribe((phList: SelectList) => this.publishigHouseSelectList = phList);
    this.publishedBook = new PublishedBook();
    this.publishedBook.id = -1;
  }


}
