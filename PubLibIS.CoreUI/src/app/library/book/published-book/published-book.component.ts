import { Component, OnInit, Inject } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { PublishedBookService } from './published-book.service';
import { PublishedBook } from '../shared/published-book.model';
import { resetFakeAsyncZone, fakeAsync } from '@angular/core/testing';
import * as $ from "jquery";

import { FormGroup, FormControl, Validators } from '@angular/forms';

import { GridDataResult, SelectionEvent } from '@progress/kendo-angular-grid';
import { State, process } from '@progress/kendo-data-query';
import { map } from 'rxjs/operators/map';
import { Observable } from 'rxjs/Observable';
import { SelectableSettings } from '@progress/kendo-angular-grid/dist/es2015/selection/selectable-settings';
import { Button } from "@progress/kendo-angular-buttons";
import { PublishingHouse } from '../../publishing-house/publishing-house.model';
import window from '@progress/kendo-popup-common/dist/npm/window';

@Component({
  templateUrl: './published-book.component.html',
  providers: [PublishedBookService],
  styleUrls: []
})
export class PublishedBookComponent implements OnInit {
  editedPublishedBook: PublishedBook;   // изменяемый товар
  public view: Observable<GridDataResult>;         // массив товаров
  loaded: boolean = true;
  jsonBackUpText: string;
  selectedIds: number[] = Array<number>();
  publishedPublishedBookService: PublishedBookService;
  public phList: PublishingHouse[];
  public gridState: State = {
    sort: [],
    skip: 0,
    take: 10
  };
  public selectableSettings: SelectableSettings = {
    checkboxOnly: false,
    mode: 'multiple'
  };

  private editedRowIndex: number;

  constructor( @Inject(PublishedBookService) publishedPublishedBookServiceFactory: any, activatedRoute: ActivatedRoute) {
    this.publishedPublishedBookService = publishedPublishedBookServiceFactory;
    this.publishedPublishedBookService.bookId = activatedRoute.snapshot.params['id'];
  }
  public goBack() {
    location.href += '../../../';
  }
  public ngOnInit(): void {
    this.view = this.publishedPublishedBookService.pipe(map(data => process(data, this.gridState)));
    this.publishedPublishedBookService.getPublishingHousehList().subscribe(author => {
      this.phList = author;
    });
    this.publishedPublishedBookService.read();

  }

  public onStateChange(state: State) {
    this.gridState = state;
    console.log(this.gridState);
    this.publishedPublishedBookService.read();
  }
  public selectionChange(event: SelectionEvent) {
    event.selectedRows.forEach(e => this.selectedIds.push(e.dataItem.id));
    event.deselectedRows.forEach(e => this.selectedIds = this.selectedIds.filter(item => item != e.dataItem.id));
    console.log(this.selectedIds);
  }

  public addHandler({ sender }, formInstance) {
    formInstance.reset();
    this.closeEditor(sender);
    var newPublishedBook = new PublishedBook();
    sender.addRow(newPublishedBook);
  }

  public editHandler({ sender, rowIndex, dataItem }) {
    this.closeEditor(sender);
    this.editedRowIndex = rowIndex;
    this.editedPublishedBook = Object.assign({}, dataItem);
    console.log(sender);
    sender.editRow(rowIndex);
  }

  public cancelHandler({ sender, rowIndex }) {
    this.closeEditor(sender, rowIndex);
  }

  public saveHandler({ sender, rowIndex, dataItem, isNew }) {
    let item: PublishedBook = dataItem;
    item.publishingHouse_Id = item.publishingHouse.id;
    this.publishedPublishedBookService.save(item, isNew);

    sender.closeRow(rowIndex);

    this.editedRowIndex = undefined;
    this.editedPublishedBook = undefined;
  }

  public removeHandler({ dataItem }) {
    this.publishedPublishedBookService.remove(dataItem);
  }
  public multiselectValueChange() {

  }

  private closeEditor(grid, rowIndex = this.editedRowIndex) {
    grid.closeRow(rowIndex);
    this.publishedPublishedBookService.resetItem(this.editedPublishedBook);
    this.editedRowIndex = undefined;
    this.editedPublishedBook = undefined;
  }
  ChooseButtonClick() {
    $("input[type=file]").click();
  }
  getJson() {
    console.log(this.publishedPublishedBookService.getJson(this.selectedIds));
  }
  setJson() {
    console.log(this.jsonBackUpText);
    this.publishedPublishedBookService.setJson(this.jsonBackUpText);
  }

  setFile(event: any) {
    let files: FileList = event.target.files;
    let blob: Blob = files.item(0).slice(0, files.item(0).size, files.item(0).type);
    let reader = new FileReader();
    reader.addEventListener("load", () => {
      this.jsonBackUpText = reader.result;
      console.log(this.jsonBackUpText)
    }, false);
    reader.readAsText(blob, "");
    console.log(this.jsonBackUpText)
  }
}
