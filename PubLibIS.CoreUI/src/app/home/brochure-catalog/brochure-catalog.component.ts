import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { CatalogService } from '../shared/catalog.service';
import * as $ from "jquery";
import { BrochureCatalog } from './brochure-catalog.model';

@Component({
  selector:"brochure-catalog",
  templateUrl: './brochure-catalog.component.html',
  providers: [CatalogService],
  styleUrls: []
})
export class BrochureCatalogComponent implements OnInit {
  brochureCatalog?: BrochureCatalog;  
  loaded: boolean = false;

  constructor(private dataService: CatalogService) {
    this.brochureCatalog = new BrochureCatalog();
    this.brochureCatalog.skip = 0;
  }

  ngOnInit() {
    this.loaded = false;
    this.loadCatalog();
    this.loaded = true;
  }

  loadCatalog() {
    this.dataService.getBrochureCatalog(this.brochureCatalog.skip, 4).subscribe((catalog: BrochureCatalog) => this.brochureCatalog = catalog);
  }

  nextPage() {
    if (this.brochureCatalog.hasNextPage) {
      this.brochureCatalog.skip++;
      this.loadCatalog();
    }
  }
  prevPage() {
    if (this.brochureCatalog.skip>0) {
      this.brochureCatalog.skip--;
      this.loadCatalog();
    }
  }
}
