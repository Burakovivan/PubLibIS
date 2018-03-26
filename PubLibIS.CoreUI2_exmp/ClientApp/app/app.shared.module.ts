import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';

import { AppComponent } from './components/app/app.component';
import { NavMenuComponent } from './components/navmenu/navmenu.component';
import { HomeComponent } from './components/home/home.component';
import { HttpModule } from '@angular/http';

import { AuthorComponent } from './components/author/author.component';
import { PublishingHouseComponent } from './components/publishing-house/publishing-house.component';
import { BrochureComponent } from './components/brochure/brochure.component';
import { BookComponent } from './components/book/book.component';
import { PublishedBookComponent } from './components/published-book/published-book.component';
import { PeriodicalComponent} from './components/periodical/periodical.component';

@NgModule({
    declarations: [
        AppComponent,
        NavMenuComponent,
        HomeComponent,


        AuthorComponent,
        PublishingHouseComponent,
        BrochureComponent,
        BookComponent,
        PublishedBookComponent,
        PeriodicalComponent
    ],
    imports: [

        CommonModule,
        FormsModule,
        HttpModule,
        RouterModule.forRoot([
            { path: '', redirectTo: 'home', pathMatch: 'full' },
            { path: 'home', component: HomeComponent },
            { path: 'author', component: AuthorComponent },
            { path: 'ph', component: PublishingHouseComponent },
            { path: 'brochure', component: BrochureComponent },
            { path: 'book', component: BookComponent },
            { path: 'periodical', component: PeriodicalComponent },
            { path: 'book/publication/:id', component: PublishedBookComponent },
            { path: '**', redirectTo: 'home' }
        ], ),


    ]
})
export class AppModuleShared {
}
