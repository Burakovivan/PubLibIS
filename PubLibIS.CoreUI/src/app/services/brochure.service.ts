import { Injectable } from '@angular/core';
import { Http, RequestOptions, RequestOptionsArgs, Headers } from '@angular/http';
import { Brochure } from '../models/brochure';
import 'rxjs/add/operator/map';

@Injectable()
export class BrochureService {

    private url = "/api/brochure";

    constructor(private http: Http) {
    }

    getBrochureList() {
        return this.http.get(this.url).map(res => res.json());
    }

    getBrochure(id: number) {
        var a: Brochure = new Brochure();
        this.http.get(this.url + '/' + id);
        return a;
    }

    getPublishingHouseSelectList(id:number) {
        return this.http.get(this.url + '/phlist/' + id).map(res => res.json());
    }
    createBrochure(Brochure: Brochure) {
        Brochure.id = 0;
        return this.http.post(this.url, Brochure).map(res => res.json());
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

    let headers = new Headers({ 'Content-Type': 'application/json' });
    let options = new RequestOptions({ headers: headers });
    var postObj = { json: json };
    this.http.post(this.url + '/setjson', postObj, options).subscribe(() => { });
  }
}
