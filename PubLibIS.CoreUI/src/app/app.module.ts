import { BrowserModule } from '@angular/platform-browser';
import { NgModule, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { RouterModule, Routes } from "@angular/router"
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { routing } from './app-routing.module'
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { JwtInterceptor } from './shared/jwt.interceptor';
import { TokenInterceptor } from './shared/token.interceptor';


import { LibraryModule } from './library/library.module';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './navmenu/navmenu.component';
import { HomeComponent } from './home/home.component';
import { BookCatalogComponent } from './home/book-catalog/book-catalog.component';
import { BrochureCatalogComponent } from './home/brochure-catalog/brochure-catalog.component';
import { PeriodicalCatalogComponent } from './home/periodical-catalog/periodical-catalog.component';
import { AccountComponent } from './account/account.component';
import { AuthenticateComponent } from './account/authenticate/authenticate.component';

import { AccountService } from './account/account.service';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,

    HomeComponent,
    BookCatalogComponent,
    BrochureCatalogComponent,
    PeriodicalCatalogComponent,

    AccountComponent,
    AuthenticateComponent,
  ],

  schemas: [CUSTOM_ELEMENTS_SCHEMA],
  imports: [

    routing,
    RouterModule,
    LibraryModule,
    
    BrowserModule,
    FormsModule,
    HttpClientModule
  ],
  providers: [
    AccountService,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: TokenInterceptor,
      multi: true
    },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: JwtInterceptor,
      multi: true
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }

