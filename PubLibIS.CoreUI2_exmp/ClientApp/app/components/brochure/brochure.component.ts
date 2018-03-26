import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { BrochureService } from '../../services/brochure.service';
import { Brochure } from '../../../../models/brochure';
import { SelectList } from '../../../../models/selectList';
import { resetFakeAsyncZone, fakeAsync } from '@angular/core/testing';

@Component({
    templateUrl: './brochure.component.html',
    providers: [BrochureService],
    styleUrls: ['../../styles/common.css']
})
export class BrochureComponent implements OnInit {
    brochure: Brochure = new Brochure();
    brochureList: Brochure[];
    publishigHouseSelectList: SelectList;
    publishigHouseSelectItemList: { value: number, selected: boolean, text: string }[];
    loaded: boolean = false;


    constructor(private dataService: BrochureService) { }

    ngOnInit() {
        this.loadList();
    }

    loadList() {
        this.loaded = false;
        this.dataService.getBrochureList().subscribe(Brochures => this.brochureList = Brochures);
        this.loaded = true;
    }

    save() {
        if (this.brochure.id == null || this.brochure.id == -1) {
            let newBrochure: Brochure = new Brochure();
            this.dataService.createBrochure(this.brochure).subscribe(Brochure => newBrochure = Brochure)
            this.brochureList.push(newBrochure);
        } else {
            this.dataService.updateBrochure(this.brochure).subscribe(data =>
                this.loadList());
        }
        this.cancel();
    }

    editItem(a: Brochure) {
        this.brochure = a;
        this.dataService.getPublishingHouseSelectList(this.brochure.id as number).subscribe(phs => this.publishigHouseSelectList = phs);
         
    }

    cancel() {
        this.brochure = new Brochure();
    }

    delete(p: Brochure) {
        this.dataService.deleteBrochure(p.id as number)
            .subscribe(data => this.loadList());
    }

    create() {
        this.brochure = new Brochure();
        this.brochure.id = -1;
    }
}