import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import { Periodical } from '../../../models/periodical';
import { PeriodicalType } from '../../../models/periodicalType';
import 'rxjs/add/operator/map';

@Injectable()
export class PeriodicalService {

    private url = "/api/periodical";

    constructor(private http: Http) {
    }

    getPeriodicalList() {
        return this.http.get(this.url).map(res => res.json());
    }

    getPeriodical(id: number) {
        var a: Periodical = new Periodical();
        this.http.get(this.url + '/' + id);
        return a;
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
}