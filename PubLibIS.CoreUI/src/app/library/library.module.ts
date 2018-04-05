//system
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { AngularMultiSelectModule } from 'angular2-multiselect-dropdown/angular2-multiselect-dropdown';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HttpClient, HttpClientModule, HttpClientJsonpModule } from '@angular/common/http';

//kendo-ui
import { GridModule } from '@progress/kendo-angular-grid';
import { ButtonsModule } from "@progress/kendo-angular-buttons";
import { DropDownsModule } from '@progress/kendo-angular-dropdowns';

// services
import { AuthorService } from './author/author.service';
import { BookService } from './book/book.service';
import { BrochureService } from './brochure/brochure.service';
import { PeriodicalService } from './periodical/periodical.service';
import { PublishingHouseService } from './publishing-house/publishing-house.service';
import { AccountService } from '../account/account.service';

//components
import { AuthorComponent } from './author/author.component';
import { BookComponent } from './book/book.component';
import { PublishedBookComponent } from './book/published-book/published-book.component';
import { BrochureComponent } from './brochure/brochure.component';
import { PeriodicalComponent } from './periodical/periodical.component';
import { PublishedPeriodicalComponent } from './periodical/published-periodial/published-periodical.component';
import { PublishingHouseComponent } from './publishing-house/publishing-house.component';
import { OnlyLoggedInUsersGuard } from '../shared/only-logged-in-users.guard';
import { LibraryRoutingModule } from './library.routing';



@NgModule({
  imports: [
    LibraryRoutingModule,
    CommonModule,
    FormsModule,
    AngularMultiSelectModule,
    
    BrowserAnimationsModule,

    HttpClientModule,
    HttpClientJsonpModule,

    ButtonsModule,
    DropDownsModule,
    GridModule
  ],

  declarations: [

    AuthorComponent,
    BookComponent,
    PublishedBookComponent,
    BrochureComponent,
    PeriodicalComponent,
    PublishedPeriodicalComponent,
    PublishingHouseComponent,
  ],

  providers: [
    OnlyLoggedInUsersGuard,
    BookService,
    BrochureService,
    PeriodicalService,
    PublishingHouseService,

    {
      deps: [HttpClient],
      provide: AuthorService,
      useFactory: (jsonp: HttpClient) => () => new AuthorService(jsonp)
    }
  ]
})

export class LibraryModule { }
