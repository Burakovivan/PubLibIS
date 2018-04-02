import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { PeriodicalService } from '../../../services/periodical.service';
import { PublishedPeriodical } from '../../../models/publishedPeriodical';
import { SelectList } from '../../../models/selectList';
import { resetFakeAsyncZone, fakeAsync } from '@angular/core/testing';
import { Periodical } from '../../../models/periodical';

@Component({
    templateUrl: './published-periodical.component.html',
    providers: [PeriodicalService],
    styleUrls: ['../../styles/common.css']
})
export class PublishedPeriodicalComponent implements OnInit {
  publishedPeriodical: PublishedPeriodical = new PublishedPeriodical();
    publishedPeriodicalList: PublishedPeriodical[] = new Array<PublishedPeriodical>();
  loaded: boolean = false;
  periodical_id: number;
  periodical: Periodical;


  constructor(private dataService: PeriodicalService, private activatedRoute: ActivatedRoute) {
    this.periodical_id = activatedRoute.snapshot.params['id'];
    this.dataService.getPeriodical(this.periodical_id).subscribe(p => this.periodical = p);
  }

    ngOnInit() {
        this.loadList();
    }

    loadList() {
      this.loaded = false;
      this.dataService.getPublicationList(this.periodical_id).subscribe((PublishedPeriodicals: PublishedPeriodical[]) => this.publishedPeriodicalList = PublishedPeriodicals);
        this.loaded = true;
    }

    save() {
      if (this.publishedPeriodical.id == null || this.publishedPeriodical.id == -1) {
        this.publishedPeriodical.periodical_Id = this.periodical_id;
        this.dataService.createPublication(this.publishedPeriodical).subscribe(()=>this.loadList())

        } else {
          this.dataService.updatePublication(this.publishedPeriodical).subscribe(data =>
                this.loadList());
        }
        this.cancel();
    }

    editItem(a: PublishedPeriodical) {
        this.publishedPeriodical = a;
    }

    cancel() {
        this.publishedPeriodical = new PublishedPeriodical();
    }

  delete(p: PublishedPeriodical) {
    this.dataService.deletePublication(p.id as number)
            .subscribe(data => this.loadList());
    }

    create() {
        this.publishedPeriodical = new PublishedPeriodical();
        this.publishedPeriodical.id = -1;
    }
}
