import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { CatalogService } from '../shared/catalog.service';
import * as $ from "jquery";
import { PeriodicalCatalog } from './periodical-catalog.model';

@Component({
  selector:"periodical-catalog",
  templateUrl: './periodical-catalog.component.html',
  providers: [CatalogService],
  styleUrls: []
})
export class PeriodicalCatalogComponent implements OnInit {
  periodicalCatalog?: PeriodicalCatalog;  
  loaded: boolean = false;

  constructor(private dataService: CatalogService) {
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
    this.dataService.getPeriodicalCatalog(this.periodicalCatalog.skip, 4).subscribe((catalog: PeriodicalCatalog) => { this.periodicalCatalog = catalog; console.log(catalog); });
    
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
