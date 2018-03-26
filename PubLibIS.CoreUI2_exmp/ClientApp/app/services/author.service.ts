import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import { Author } from '../../../models/author';
import 'rxjs/add/operator/map';

@Injectable()
export class AuthorService {

    private url = "/api/author";

    constructor(private http: Http) {
    }

    getAuthorList() {
        return this.http.get(this.url).map(res => res.json());
    }

    getAuthor(id: number) {
        var a: Author = new Author();
        this.http.get(this.url + '/' + id);
        return a;
    }

    createAuthor(author: Author) {
        author.id = 0;
        return this.http.post(this.url, author).map(res => res.json());
    }

    updateAuthor(author: Author) {
        return this.http.put(this.url + '/', author);
    }

    deleteAuthor(id: number) {
        return this.http.delete(this.url + '/' + id);
    }
}