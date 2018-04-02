import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { PeriodicalService } from '../../../services/periodical.service';
import * as $ from "jquery";
import { PeriodicalCatalog } from './periodicalCatalog';

@Component({
  selector:"periodical-catalog",
  templateUrl: './periodical-catalog.component.html',
  providers: [PeriodicalService],
  styleUrls: ['../../styles/common.css']
})
export class PeriodicalCatalogComponent implements OnInit {
  periodicalCatalog?: PeriodicalCatalog;  
  loaded: boolean = false;

  constructor(private dataService: PeriodicalService) {
    this.periodicalCatalog = new PeriodicalCatalog();
    this.periodicalCatalog.skip = 0;
  }

  ngOnInit() {
    this.loaded = false;
    this.loadCatalog();
    this.loaded = true;
  }

  loadCatalog() {
    console.log("start");
    this.dataService.getCatalog(this.periodicalCatalog.skip, 4).subscribe((catalog: PeriodicalCatalog) => { this.periodicalCatalog = catalog; console.log(catalog); });
    
  }

  nextPage() {
    if (this.periodicalCatalog.hasNextPage) {
      this.periodicalCatalog.skip++;
      this.loadCatalog();
    }
  }
  prevPage() {
    if (this.periodicalCatalog.skip>0) {
      this.periodicalCatalog.skip--;
      this.loadCatalog();
    }
  }
}
