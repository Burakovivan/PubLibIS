import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { CatalogService } from '../shared/catalog.service';
import { BookCatalog } from './book-catalog.model';

@Component({
  
  selector:"book-catalog",
  templateUrl: './book-catalog.component.html',
  providers: [CatalogService],
  styleUrls: []
})
export class BookCatalogComponent implements OnInit {
  bookCatalog?: BookCatalog;  
  loaded: boolean = false;

  constructor(private dataService: CatalogService) {
    this.bookCatalog = new BookCatalog();
    this.bookCatalog.skip = 0;
  }

  ngOnInit() {
    this.loaded = false;
    this.loadCatalog();
    this.loaded = true;
  }

  loadCatalog() {
    this.dataService.getBookCatalog(this.bookCatalog.skip, 4).subscribe(catalog => this.bookCatalog = catalog);
  }

  nextPage() {
    if (this.bookCatalog.hasNextPage) {
      this.bookCatalog.skip++;
      this.loadCatalog();
    }
  }
  prevPage() {
    if (this.bookCatalog.skip>0) {
      this.bookCatalog.skip--;
      this.loadCatalog();
    }
  }
}
