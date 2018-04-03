import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Author } from './author.model';
import 'rxjs/add/operator/map';
import { Observable } from 'rxjs/Observable';

@Injectable()
export class AuthorService {

  private url = "/api/author";

  constructor(private http: HttpClient) {
  }

  getAuthorList() {
    return this.http.get(this.url);
  }

  getAuthor(id: number) {
    var a: Author = new Author();
    this.http.get(this.url + '/' + id);
    return a;
  }

  createAuthor(author: Author) {
    author.id = 0;
    return this.http.post(this.url, author);
  }

  updateAuthor(author: Author) {
    return this.http.put(this.url + '/', author);
  }

  deleteAuthor(id: number) {
    return this.http.delete(this.url + '/' + id);
  }

  getJson(ids: number[] | string[]) {
    var url = this.url + '/getjson';
    console.log(url);
    return this.http.post(this.url + '/getjson', ids).subscribe(() => { });
  }
  setJson(json: string) {

    let headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    var postObj = { json: json };
    this.http.post(this.url + '/setjson', postObj, { headers: headers}).subscribe(() => { });
  }

}
