import { HttpModule } from '@angular/http';
import { FormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { AngularMultiSelectModule } from 'angular2-multiselect-dropdown/angular2-multiselect-dropdown';
import { NgModule, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';

import { AuthService } from './services/auth/auth.service';
import { AppComponent } from './components/app/app.component';
import { HomeComponent } from './components/home/home.component';
import { AuthComponent } from './components/auth/auth.component';
import { BookComponent } from './components/book/book.component';
import { JwtInterceptor } from './services/auth/jwt.interceptor';
import { AuthorComponent } from './components/author/author.component';
import { NavMenuComponent } from './components/navmenu/navmenu.component';
import { TokenInterceptor } from './services/auth/token.interceptor';
import { AccountComponent } from './components/account/account.component';
import { BrochureComponent } from './components/brochure/brochure.component';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { PublishedPeriodical } from './models/publishedPeriodical';
import { PeriodicalComponent } from './components/periodical/periodical.component';
import { RouterModule, Routes } from "@angular/router"
import { BookCatalogComponent } from './components/book-catalog/book-catalog.component';
import { PublishedBookComponent } from './components/published-book/published-book.component';
import { PublishingHouseComponent } from './components/publishing-house/publishing-house.component';
import { BrochureCatalogComponent } from './components/brochure-catalog/brochure-catalog.component';
import { PeriodicalCatalogComponent } from './components/periodical-catalog/periodical-catalog.component';
import { PublishedPeriodicalComponent } from './components/published-periodial/published-periodical.component';

const appRoutes: Routes = [
  { path: 'home', component: HomeComponent, data: { title: "Home" } },
  { path: 'author', component: AuthorComponent, data: { title: "Authors" } },
  { path: 'book', component: BookComponent, data: { title: "Books" } },
  { path: 'book/publication/:id', component: PublishedBookComponent, data: { title: "Book publications" } },
  { path: 'brochure', component: BrochureComponent, data: { title: "Brochures" } },
  { path: 'periodical', component: PeriodicalComponent, data: { title: "Periodicals" } },
  { path: 'periodical/publication/:id', component: PublishedPeriodicalComponent, data: { title: "Periodical publications" } },
  { path: 'ph', component: PublishingHouseComponent, data: { title: "Publishing houses" } },
  { path: 'account', component: AuthComponent, data: { title: "Account" } },
  {
    path: '',
    redirectTo: 'home',
    pathMatch: 'full'
  },
  { path: '**', component: HomeComponent }
];

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    AccountComponent,

    BookCatalogComponent,
    BrochureCatalogComponent,
    PeriodicalCatalogComponent,

    HomeComponent,
    AuthorComponent,
    BookComponent,
    BrochureComponent,
    PeriodicalComponent,
    PublishedBookComponent,
    PeriodicalComponent,
    PublishedPeriodicalComponent,
    PublishingHouseComponent,
    AuthComponent
  ],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
  imports: [
    AngularMultiSelectModule,
    BrowserModule,
    FormsModule,
    HttpModule,
    HttpClientModule,
    RouterModule.forRoot(
      appRoutes,
      { enableTracing: true } // <-- debugging purposes only
    )
  ],
  providers: [
    AuthService,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: TokenInterceptor,
      multi: true
    }, {
      provide: HTTP_INTERCEPTORS,
      useClass: JwtInterceptor,
      multi: true
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }

