import { Injectable } from '@angular/core';
import { Http, RequestOptions, RequestOptionsArgs, Headers } from '@angular/http';
import { PublishingHouse } from '../models/publishingHouse';
import 'rxjs/add/operator/map';

@Injectable()
export class PublishingHouseService {

    private url = "/api/publishingHouse";

    constructor(private http: Http) {
    }

    getPublishingHouseList() {
        return this.http.get(this.url).map(res => res.json());
    }

    getPublishingHouse(id: number) {
        var a: PublishingHouse = new PublishingHouse();
        this.http.get(this.url + '/' + id);
        return a;
    }

    createPublishingHouse(PublishingHouse: PublishingHouse) {
        PublishingHouse.id = 0;
        return this.http.post(this.url, PublishingHouse).map(res => res.json());
    }

    updatePublishingHouse(PublishingHouse: PublishingHouse) {
        return this.http.put(this.url + '/', PublishingHouse);
    }

    deletePublishingHouse(id: number) {
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
