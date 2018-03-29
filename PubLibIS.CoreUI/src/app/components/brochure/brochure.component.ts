import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { BrochureService } from '../../services/brochure.service';
import { Brochure } from '../../models/brochure';
import { SelectList } from '../../models/selectList';
import { resetFakeAsyncZone, fakeAsync } from '@angular/core/testing';
import * as $ from "jquery";

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
  jsonBackUpText: string;


    constructor(private dataService: BrochureService) { }

    ngOnInit() {
        this.loadList();
    }

    loadList() {
      this.loaded = false;
      this.dataService.getBrochureList().subscribe((Brochures: Brochure[]) => this.brochureList = Brochures);
        this.loaded = true;
    }

    save() {
        if (this.brochure.id == null || this.brochure.id == -1) {
          let newBrochure: Brochure = new Brochure();
          this.dataService.createBrochure(this.brochure).subscribe((brochure: Brochure) => this.brochureList.push(brochure),
            (err: { error?: { message?: string } }) => { alert(err.error.message); this.loadList() })
           ;
        } else {
          this.dataService.updateBrochure(this.brochure).subscribe(data =>
            this.loadList(), err => this.loadList());
        }
        this.cancel();
    }

    editItem(a: Brochure) {
      this.brochure = a;
      this.dataService.getPublishingHouseSelectList(this.brochure.id as number).subscribe((phs: SelectList) => this.publishigHouseSelectList = phs);
         
    }

    cancel() {
        this.brochure = new Brochure();
    }

    delete(p: Brochure) {
        this.dataService.deleteBrochure(p.id as number)
            .subscribe(data => this.loadList());
    }

    create() {
      this.dataService.getPublishingHouseSelectList(this.brochure.id as number).subscribe((phs: SelectList) => this.publishigHouseSelectList = phs);
      this.brochure = new Brochure();
      console.log("hello");
        this.brochure.id = -1;
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
