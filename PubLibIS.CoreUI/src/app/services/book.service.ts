import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Book } from '../models/book';
import 'rxjs/add/operator/map';
import { PublishedBook } from '../models/publishedBook';

@Injectable()
export class BookService {

    private url = "/api/book";
    private publicationUrl = "/api/publishedBook";

  constructor(private http: HttpClient) {
        

    }
    getBook(id: number) {
        this.url = window.location.origin + this.url;
        return this.http.get(this.url + '/' + id);
    }

    getBookList() {
        return this.http.get(this.url);
    }


    createBook(book: Book) {
        return this.http.post(this.url, book);
    }

    updateBook(book: Book) {
        return this.http.put(this.url + '/', book);
    }

    deleteBook(id: number) {
        return this.http.delete(this.url + '/' + id);
    }

    getPublicationList(id:number) {
        return this.http.get(this.publicationUrl+'/'+id);
    }
    getPublishingHouseSelectList(id: number) {
        return this.http.get(this.publicationUrl + '/phlist/' + id);
    }

    createPublication(book: PublishedBook) {
        book.id = 0;
        return this.http.post(this.publicationUrl, book);
    }

    updatePublication(book: PublishedBook) {
        return this.http.put(this.publicationUrl + '/', book);
    }

    deletePublication(id: number) {
        return this.http.delete(this.publicationUrl + '/' + id);
  }
  getJson(ids: number[] | string[]) {
    var url = this.url + '/getjson';
    console.log(url);
    return this.http.post(this.url + '/getjson', ids).subscribe(() => { });
  }
  setJson(json: string) {

    let headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    var postObj = { json: json };
    this.http.post(this.url + '/setjson', postObj, { headers: headers }).subscribe(() => { });
  }
}
