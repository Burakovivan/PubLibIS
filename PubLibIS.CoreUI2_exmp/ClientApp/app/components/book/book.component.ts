import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { BookService } from '../../services/book.service';
import { Book } from '../../../../models/book';
import { resetFakeAsyncZone, fakeAsync } from '@angular/core/testing';
import { SelectList } from '../../../../models/selectList';

@Component({
    templateUrl: './book.component.html',
    providers: [BookService],
    styleUrls: ['../../styles/common.css']
})
export class BookComponent implements OnInit {
    book: Book = new Book();   // изменяемый товар
    books: Book[];                // массив товаров
    loaded: boolean = true;

    constructor(private dataService: BookService) {
    }

    ngOnInit() {
       
        this.loadBooks();    // загрузка данных при старте компонента  
    }
    // получаем данные через сервис
    loadBooks() {
        this.loaded = false;
        this.dataService.getBookList().subscribe(books => this.books = books);
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
}