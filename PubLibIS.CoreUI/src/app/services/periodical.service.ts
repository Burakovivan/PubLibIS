import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Periodical } from '../models/periodical';
import { PeriodicalType } from '../models/periodicalType';
import 'rxjs/add/operator/map';
import { PublishedPeriodical } from '../models/publishedPeriodical';

@Injectable()
export class PeriodicalService {

  private url = "/api/periodical";
  private publicationUrl = "/api/publishedperiodical"

  constructor(private http: HttpClient) {
  }

  getPeriodicalList() {
    return this.http.get(this.url);
  }
  getCatalog(skip: number = null, take: number = null) {
    return this.http.get(this.url + '/getcatalog/?skip=' + skip + "&take=" + take);
  }
  getPeriodical(id: number) {
    return this.http.get(this.url + '/' + id);
  }

  createPeriodical(periodical: Periodical) {
    periodical.id = 0;
    return this.http.post(this.url, periodical);
  }

  updatePeriodical(periodical: Periodical) {
    return this.http.put(this.url + '/', periodical);
  }

  deletePeriodical(id: number) {
    return this.http.delete(this.url + '/' + id);
  }

  getPublishingHouseSelectList(id: number) {
    return this.http.get(this.url + '/phlist/' + id);
  }

  getTypeSelectList(id: number) {
    return this.http.get(this.url + '/typelist/' + id);
  }

  getPublicationList(id: number) {
    return this.http.get(this.publicationUrl + '/' + id);
  }

  createPublication(pp: PublishedPeriodical) {
    pp.id = 0;
    return this.http.post(this.publicationUrl, pp);
  }

  updatePublication(pp: PublishedPeriodical) {
    return this.http.put(this.publicationUrl + '/', pp);
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
