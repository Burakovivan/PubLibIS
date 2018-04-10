import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Periodical } from './shared/periodical.model';
import { PeriodicalType } from './shared/periodical-type.model';
import 'rxjs/add/operator/map';
import { PublishedPeriodical } from './shared/published-periodical.model';
import { BackupFile } from '../shared/file';
import { AppSetting } from '../../../app.setting';

@Injectable()
export class PeriodicalService {

  private url = AppSetting.PERIODICAL_URL;
  private publicationUrl = AppSetting.PUBLISHED_PERIODICAL_URL;

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
    return this.http.post(this.url + '/getjson', ids).subscribe((file: BackupFile) => { window.location.href = `${AppSetting.FILE_URL}/${file.fileNameBase64}` });
  }
  setJson(json: string) {

    let headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    var postObj = { json: json };
    this.http.post(this.url + '/setjson', postObj, { headers: headers }).subscribe(() => { });
  }
}
