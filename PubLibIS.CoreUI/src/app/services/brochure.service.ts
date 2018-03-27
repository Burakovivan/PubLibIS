import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Brochure } from '../models/brochure';
import 'rxjs/add/operator/map';

@Injectable()
export class BrochureService {

    private url = "/api/brochure";

  constructor(private http: HttpClient) {
    }

    getBrochureList() {
        return this.http.get(this.url);
    }

    getBrochure(id: number) {
        var a: Brochure = new Brochure();
        this.http.get(this.url + '/' + id);
        return a;
    }

    getPublishingHouseSelectList(id:number) {
        return this.http.get(this.url + '/phlist/' + id);
    }
    createBrochure(Brochure: Brochure) {
        Brochure.id = 0;
        return this.http.post(this.url, Brochure);
    }

    updateBrochure(Brochure: Brochure) {
        return this.http.put(this.url + '/', Brochure);
    }

    deleteBrochure(id: number) {
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
    this.http.post(this.url + '/setjson', postObj, { headers: headers }).subscribe(() => { });
  }
}
