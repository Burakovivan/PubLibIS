import { Injectable } from '@angular/core';
import { Http,  } from '@angular/http';
import { Book } from '../../../models/book';
import 'rxjs/add/operator/map';
import { PublishedBook } from '../../../models/publishedBook';

@Injectable()
export class BookService {

    private url = "/api/book";
    private publicationUrl = "/api/publishedBook";

    constructor(private http: Http) {
        

    }
    getBook(id: number) {
        this.url = window.location.origin + this.url;
        return this.http.get(this.url + '/' + id).map(res => res.json());
    }

    getBookList() {
        return this.http.get(this.url).map(res => res.json());
    }


    createBook(book: Book) {
        return this.http.post(this.url, book).map(res => res.json());
    }

    updateBook(book: Book) {
        return this.http.put(this.url + '/', book);
    }

    deleteBook(id: number) {
        return this.http.delete(this.url + '/' + id);
    }

    getPublicationList(id:number) {
        return this.http.get(this.publicationUrl+'/'+id).map(res => res.json());
    }
    getPublishingHouseSelectList(id: number) {
        return this.http.get(this.publicationUrl + '/phlist/' + id).map(res => res.json());
    }

    createPublication(book: PublishedBook) {
        book.id = 0;
        return this.http.post(this.publicationUrl, book).map(res => res.json());
    }

    updatePublication(book: PublishedBook) {
        return this.http.put(this.publicationUrl + '/', book);
    }

    deletePublication(id: number) {
        return this.http.delete(this.publicationUrl + '/' + id);
    }
}