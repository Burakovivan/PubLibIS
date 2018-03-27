import { BrowserModule } from '@angular/platform-browser';
import { NgModule, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule, Routes } from "@angular/router"
import { AppComponent } from './components/app/app.component';
import { NavMenuComponent } from './components/navmenu/navmenu.component';
import { HomeComponent } from './components/home/home.component';
import { AuthorComponent } from './components/author/author.component';
import { BookComponent } from './components/book/book.component';
import { BrochureComponent } from './components/brochure/brochure.component';
import { PeriodicalComponent } from './components/periodical/periodical.component';
import { PublishedBookComponent } from './components/published-book/published-book.component';
import { PublishingHouseComponent } from './components/publishing-house/publishing-house.component';
import { PublishedPeriodicalComponent } from './components/published-periodial/published-periodical.component';
import { PublishedPeriodical } from './models/publishedPeriodical';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { TokenInterceptor } from './services/auth/token.interceptor';
import { AuthService } from './services/auth/auth.service';
import { AuthComponent } from './components/auth/auth.component';
import { JwtInterceptor } from './services/auth/jwt.interceptor';
import { AccountComponent } from './components/account/account.component';

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

