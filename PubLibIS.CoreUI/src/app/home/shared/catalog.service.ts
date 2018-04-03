import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import { BookCatalog } from '../book-catalog/book-catalog.model';

@Injectable()
export class CatalogService {
  private bookUrl = "/api/book";
  private brochureUrl = "/api/brochure";
  private periodicalUrl = "/api/periodical";

  constructor(private http: HttpClient) { }

  private mapPath(apiUrl: string, skip: number, take: number) {
    return `${apiUrl}/getcatalog/?skip=${skip}&take=${take}`;
  }

  getBookCatalog(skip: number, take: number): Observable<BookCatalog> {
    return this.http.get(this.mapPath(this.bookUrl, skip, take));
  }

  getBrochureCatalog(skip: number, take: number) {
    return this.http.get(this.mapPath(this.brochureUrl, skip, take));
  }

  getPeriodicalCatalog(skip: number, take: number) {
    return this.http.get(this.mapPath(this.periodicalUrl, skip, take));
  }

}
