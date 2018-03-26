import { Injectable } from '@angular/core';
import { Http, RequestOptions, RequestOptionsArgs, Headers } from '@angular/http';
import { Periodical } from '../models/periodical';
import { PeriodicalType } from '../models/periodicalType';
import 'rxjs/add/operator/map';
import { PublishedPeriodical } from '../models/publishedPeriodical';

@Injectable()
export class PeriodicalService {

  private url = "/api/periodical";
  private publicationUrl = "/api/publishedperiodical"

  constructor(private http: Http) {
  }

  getPeriodicalList() {
    return this.http.get(this.url).map(res => res.json());
  }

  getPeriodical(id: number) {
    return this.http.get(this.url + '/' + id).map(res => res.json());
  }

  createPeriodical(periodical: Periodical) {
    periodical.id = 0;
    return this.http.post(this.url, periodical).map(res => res.json());
  }

  updatePeriodical(periodical: Periodical) {
    return this.http.put(this.url + '/', periodical);
  }

  deletePeriodical(id: number) {
    return this.http.delete(this.url + '/' + id);
  }

  getPublishingHouseSelectList(id: number) {
    return this.http.get(this.url + '/phlist/' + id).map(res => res.json());
  }

  getTypeSelectList(id: number) {
    return this.http.get(this.url + '/typelist/' + id).map(res => res.json());
  }

  getPublicationList(id: number) {
    return this.http.get(this.publicationUrl + '/' + id).map(res => res.json());
  }

  createPublication(pp: PublishedPeriodical) {
    pp.id = 0;
    return this.http.post(this.publicationUrl, pp).map(res => res.json());
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

    let headers = new Headers({ 'Content-Type': 'application/json' });
    let options = new RequestOptions({ headers: headers });
    var postObj = { json: json };
    this.http.post(this.url + '/setjson', postObj, options).subscribe(() => { });
  }
}
