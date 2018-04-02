import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { PeriodicalService } from '../../services/periodical.service';
import { Periodical } from '../../models/periodical';
import { SelectList } from '../../models/selectList';
import { resetFakeAsyncZone, fakeAsync } from '@angular/core/testing';
import * as $ from "jquery";
import { PeriodicalType } from '../../models/periodicalType';

@Component({
    templateUrl: './periodical.component.html',
    providers: [PeriodicalService],
    styleUrls: ['../../styles/common.css']
})
export class PeriodicalComponent implements OnInit {
    periodical: Periodical = new Periodical();
    periodicalList: Periodical[] = new Array<Periodical>();
    publishigHouseSelectList: SelectList = new SelectList();
    typeSelectList: SelectList;
    loaded: boolean = false;
  jsonBackUpText: string;


    constructor(private dataService: PeriodicalService) { }

    ngOnInit() {
        this.loadList();
    }

    loadList() {
      this.loaded = false;
      this.dataService.getPeriodicalList().subscribe((Periodicals: Periodical[])=> this.periodicalList = Periodicals);
      this.loaded = true;
    }

    save() {
        if (this.periodical.id == null || this.periodical.id == -1) {
            let newPeriodical: Periodical = new Periodical();
            this.dataService.createPeriodical(this.periodical).subscribe(Periodical => newPeriodical = Periodical)
            this.periodicalList.push(newPeriodical);
        } else {
            this.dataService.updatePeriodical(this.periodical).subscribe(data =>
                this.loadList());
        }
        this.cancel();
    }

  loadSelectList() {
    this.dataService.getPublishingHouseSelectList(this.periodical.id as number).subscribe((phList: SelectList) => this.publishigHouseSelectList = phList);
    this.dataService.getTypeSelectList(this.periodical.id as number).subscribe((typeList: SelectList) => this.typeSelectList = typeList);

  }
    editItem(a: Periodical) {
      this.periodical = a;
      this.loadSelectList();
     }

    cancel() {
        this.periodical = new Periodical();
    }

    delete(p: Periodical) {
        this.dataService.deletePeriodical(p.id as number)
            .subscribe(data => this.loadList());
    }

    create() {
      this.periodical = new Periodical();
      this.periodical.type = new PeriodicalType(0);
        this.periodical.id = -1;
      this.loadSelectList();
      console.log(this.typeSelectList);
      console.log(this.publishigHouseSelectList);
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
