import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { PublishingHouseService } from '../../services/publishing-house.service';
import { PublishingHouse } from '../../../../models/publishingHouse';
import { resetFakeAsyncZone, fakeAsync } from '@angular/core/testing';

@Component({
    templateUrl: './publishing-house.component.html',
    providers: [PublishingHouseService],
    styleUrls: ['../../styles/common.css']
})
export class PublishingHouseComponent implements OnInit {
    publishingHouse: PublishingHouse = new PublishingHouse();
    publishingHouseList: PublishingHouse[];
    loaded: boolean = false;

    constructor(private dataService: PublishingHouseService) { }

    ngOnInit() {
        this.loadList();
    }

    loadList() {
        this.loaded = false;
        this.dataService.getPublishingHouseList().subscribe(PublishingHouses => this.publishingHouseList = PublishingHouses);
        this.loaded = true;
    }

    save() {
        if (this.publishingHouse.id == null || this.publishingHouse.id == -1) {
            let newPublishingHouse: PublishingHouse = new PublishingHouse();
            this.dataService.createPublishingHouse(this.publishingHouse).subscribe(PublishingHouse => newPublishingHouse = PublishingHouse)
            this.publishingHouseList.push(newPublishingHouse);
        } else {
            this.dataService.updatePublishingHouse(this.publishingHouse).subscribe(data =>
                this.loadList());
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
}