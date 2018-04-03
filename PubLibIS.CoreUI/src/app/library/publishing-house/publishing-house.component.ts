import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { PublishingHouseService } from './publishing-house.service';
import { PublishingHouse } from './publishing-house.model';
import { resetFakeAsyncZone, fakeAsync } from '@angular/core/testing';
import * as $ from "jquery";

@Component({
    templateUrl: './publishing-house.component.html',
    providers: [PublishingHouseService],
    styleUrls: []
})
export class PublishingHouseComponent implements OnInit {
    publishingHouse: PublishingHouse = new PublishingHouse();
    publishingHouseList: PublishingHouse[];
    loaded: boolean = false;
  jsonBackUpText: string;

    constructor(private dataService: PublishingHouseService) { }

    ngOnInit() {
        this.loadList();
    }

    loadList() {
        this.loaded = false;
      this.dataService.getPublishingHouseList().subscribe((PublishingHouses: PublishingHouse[]) => this.publishingHouseList = PublishingHouses);
        this.loaded = true;
    }

    save() {
        if (this.publishingHouse.id == null || this.publishingHouse.id == -1) {
          this.dataService.createPublishingHouse(this.publishingHouse).subscribe((publishingHouse: PublishingHouse) => this.publishingHouseList.push(publishingHouse))
        } else {
            this.dataService.updatePublishingHouse(this.publishingHouse).subscribe(data =>
              this.loadList(), err => this.loadList());
        }
        this.cancel();
    }

    editItem(a: PublishingHouse) {
        this.publishingHouse = a;
    }

    cancel() {
        this.publishingHouse = new PublishingHouse();
    }

    delete(p: PublishingHouse) {
        this.dataService.deletePublishingHouse(p.id as number)
            .subscribe(data => this.loadList());
    }

    

    create() {
        this.publishingHouse = new PublishingHouse();
        this.publishingHouse.id = -1;
  }
  getJson() {
    var ids: string[] = $("input:checked[name=backup]").toArray().map((e) => e.id);
    console.log(this.dataService.getJson(ids));
    console.log(ids);
  }
  setJson() {
    console.log(this.jsonBackUpText);
    this.dataService.setJson(this.jsonBackUpText);
  }
  setFile(files: FileList) {
    let blob: Blob = files.item(0).slice(0, files.item(0).size, files.item(0).type);
    let reader = new FileReader();
    reader.addEventListener("load", () => {
      this.jsonBackUpText = reader.result;
    }, false);
    reader.readAsText(blob, "");
  }
}
