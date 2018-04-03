import { BrowserModule } from '@angular/platform-browser';
import { NgModule, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { RouterModule, Routes } from "@angular/router"
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { AngularMultiSelectModule } from 'angular2-multiselect-dropdown/angular2-multiselect-dropdown';

import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { JwtInterceptor } from './shared/jwt.interceptor';
import { TokenInterceptor } from './shared/token.interceptor';


import { AccountModule } from './account/account.module';
import { LibraryModule } from './library/library.module';

import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import { NavMenuComponent } from './navmenu/navmenu.component';

const appRoutes: Routes = [
  { path: '', redirectTo: 'home'},
  { path: '**', component: HomeComponent },
  { path: 'home', component: HomeComponent },
  { path: 'account', loadChildren: 'AccountModule' },
  { path: 'library', loadChildren: 'LibraryModule' },

];

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    NavMenuComponent,
  ],

  schemas: [CUSTOM_ELEMENTS_SCHEMA],
  imports: [
    AngularMultiSelectModule,

    AccountModule,
    LibraryModule,

    BrowserModule,
    FormsModule,
    HttpClientModule,
    RouterModule.forRoot(
      appRoutes,
      { enableTracing: true } // <-- debugging purposes only
    )
  ],
  providers: [

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

