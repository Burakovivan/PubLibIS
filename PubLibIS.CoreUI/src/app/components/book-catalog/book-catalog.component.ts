import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { BookService } from '../../services/book.service';
import * as $ from "jquery";
import { BookCatalog } from '../../models/bookCatalog';

@Component({
  selector:"book-catalog",
  templateUrl: './book-catalog.component.html',
  providers: [BookService],
  styleUrls: ['../../styles/common.css']
})
export class BookCatalogComponent implements OnInit {
  bookCatalog?: BookCatalog;  
  loaded: boolean = false;

  constructor(private dataService: BookService) {
    this.bookCatalog = new BookCatalog();
    this.bookCatalog.skip = 0;
  }

  ngOnInit() {
    this.loaded = false;
    this.loadCatalog();
    this.loaded = true;
  }

  loadCatalog() {
    this.dataService.getCatalog(this.bookCatalog.skip, 4).subscribe((catalog: BookCatalog) => this.bookCatalog = catalog);
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
