import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import { PublishingHouse } from '../../../models/publishingHouse';
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
}