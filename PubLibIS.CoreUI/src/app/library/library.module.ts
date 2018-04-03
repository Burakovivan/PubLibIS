import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { routing } from './library.routing';

import { AuthorService } from './author/author.service';
import { BookService } from './book/book.service';
import { BrochureService } from './brochure/brochure.service';
import { PeriodicalService } from './periodical/periodical.service';
import { PublishingHouseService } from './publishing-house/publishing-house.service';


@NgModule({

  imports: [
    CommonModule,
    routing
  ],

  declarations: [
    //AuthorComponent,
    //BookComponent,
    //PublishedBookComponent,
    //BrochureComponent,
    //PeriodicalComponent,
    //PublishedPeriodicalComponent,
    //PublishingHouseComponent,
  ],

  providers: [
    AuthorService,
    BookService,
    BrochureService,
    PeriodicalService,
    PublishingHouseService,
  ],

  exports: [
    //AuthorComponent,
    //BookComponent,
    //PublishedBookComponent,
    //BrochureComponent,
    //PeriodicalComponent,
    //PublishedPeriodicalComponent,
    //PublishingHouseComponent,
  ]
})

export class LibraryModule { }
