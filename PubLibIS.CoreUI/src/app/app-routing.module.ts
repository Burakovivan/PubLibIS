import { NgModule, ModuleWithProviders } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AccountComponent } from './account/account.component';
import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import { AuthenticateComponent } from './account/authenticate/authenticate.component'

const appRoutes: Routes = [

  { path: '', redirectTo: 'home', pathMatch:'full' },
  { path: 'authenticate', component: AuthenticateComponent },
  { path: 'home', component: HomeComponent },
  {
    path: 'library',
    children: [
      { path: '', loadChildren: 'app/library/library.module#LibraryModule' },
    ]
  },
  { path: '**', redirectTo: 'home', pathMatch: 'full' },

];
export const routing: ModuleWithProviders = RouterModule.forRoot(appRoutes);
