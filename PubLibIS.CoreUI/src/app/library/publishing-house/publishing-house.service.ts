import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { PublishingHouse } from './publishing-house.model';
import 'rxjs/add/operator/map';
import { BackupFile } from '../shared/file';
import { AppSetting } from '../../../app.setting';

@Injectable()
export class PublishingHouseService {

  private url = AppSetting.PUBLISHING_HOUSE_URL;

  constructor(private http: HttpClient) {
    }

    getPublishingHouseList() {
        return this.http.get(this.url);
    }

    getPublishingHouse(id: number) {
        var a: PublishingHouse = new PublishingHouse();
        this.http.get(this.url + '/' + id);
        return a;
    }

    createPublishingHouse(PublishingHouse: PublishingHouse) {
        PublishingHouse.id = 0;
        return this.http.post(this.url, PublishingHouse);
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
    return this.http.post(this.url + '/getjson', ids).subscribe((file: BackupFile) => { window.location.href = `${AppSetting.FILE_URL}/${file.fileNameBase64}` });
  }
  setJson(json: string) {

    let headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    var postObj = { json: json };
    this.http.post(this.url + '/setjson', postObj, { headers: headers }).subscribe(() => { });
  }
}
