import { ModuleWithProviders } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { AuthorComponent } from './author/author.component';
import { BookComponent } from './book/book.component';
import { PublishedBookComponent } from './book/published-book/published-book.component';
import { BrochureComponent } from './brochure/brochure.component';
import { PeriodicalComponent } from './periodical/periodical.component';
import { PublishedPeriodicalComponent } from './periodical/published-periodial/published-periodical.component';
import { PublishingHouseComponent } from './publishing-house/publishing-house.component';

const routes: Routes = [
    { path: 'author', component: AuthorComponent, data: { title: "Authors" } },
    { path: 'book', component: BookComponent, data: { title: "Books" } },
    { path: 'book/publication/:id', component: PublishedBookComponent, data: { title: "Book publications" } },
    { path: 'brochure', component: BrochureComponent, data: { title: "Brochures" } },
    { path: 'periodical', component: PeriodicalComponent, data: { title: "Periodicals" } },
    { path: 'periodical/publication/:id', component: PublishedPeriodicalComponent, data: { title: "Periodical publications" } },
    { path: 'puplishing-house', component: PublishingHouseComponent, data: { title: "Publishing houses" } },
];

export const routing: ModuleWithProviders = RouterModule.forChild(routes);
