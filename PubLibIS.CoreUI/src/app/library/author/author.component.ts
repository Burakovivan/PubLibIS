import { Component, OnInit, Inject } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { AuthorService } from './author.service';
import { Author } from './author.model';
import { resetFakeAsyncZone, fakeAsync } from '@angular/core/testing';
import * as $ from "jquery";

import { FormGroup, FormControl, Validators } from '@angular/forms';

import { GridDataResult, SelectionEvent } from '@progress/kendo-angular-grid';
import { State, process } from '@progress/kendo-data-query';
import { map } from 'rxjs/operators/map';
import { Observable } from 'rxjs/Observable';
import { SelectableSettings } from '@progress/kendo-angular-grid/dist/es2015/selection/selectable-settings';
import { Button } from "@progress/kendo-angular-buttons";

@Component({
  templateUrl: './author.component.html',
  providers: [AuthorService],
  styleUrls: []
})
export class AuthorComponent implements OnInit {
  editedAuthor: Author;   // изменяемый товар
  public view: Observable<GridDataResult>;         // массив товаров
  loaded: boolean = true;
  jsonBackUpText: string;
  selectedIds: number[] = Array<number>();
  authorService: AuthorService;
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

  constructor( @Inject(AuthorService) authorServiceFactory: any) {
    this.authorService = authorServiceFactory;
  }

  public ngOnInit(): void {
    this.view = this.authorService.pipe(map(data => process(data, this.gridState)));

    this.authorService.read();
  }

  public onStateChange(state: State) {
    this.gridState = state;
    console.log(state);
    this.authorService.read();
  }
  public selectionChange(event: SelectionEvent) {
    event.selectedRows.forEach(e => this.selectedIds.push(e.dataItem.id));
    event.deselectedRows.forEach(e => this.selectedIds = this.selectedIds.filter(item => item != e.dataItem.id));
    console.log(this.selectedIds);
  }

  public addHandler({ sender }, formInstance) {
    formInstance.reset();
    this.closeEditor(sender);
    var newAuthor = new Author();
    sender.addRow(newAuthor);
    console.log(this.view);
    console.log(sender.selectionService.selected);
  }

  public editHandler({ sender, rowIndex, dataItem }) {
    this.closeEditor(sender);
    console.log(sender);
    console.log(rowIndex);
    console.log(dataItem);
    this.editedRowIndex = rowIndex;
    this.editedAuthor = Object.assign({}, dataItem);

    sender.editRow(rowIndex);
  }

  public cancelHandler({ sender, rowIndex }) {
    this.closeEditor(sender, rowIndex);
  }

  public saveHandler({ sender, rowIndex, dataItem, isNew }) {
    this.authorService.save(dataItem, isNew);

    sender.closeRow(rowIndex);

    this.editedRowIndex = undefined;
    this.editedAuthor = undefined;
  }

  public removeHandler({ dataItem }) {
    this.authorService.remove(dataItem);
  }

  private closeEditor(grid, rowIndex = this.editedRowIndex) {
    grid.closeRow(rowIndex);
    this.authorService.resetItem(this.editedAuthor);
    this.editedRowIndex = undefined;
    this.editedAuthor = undefined;
  }
  ChooseButtonClick() {
    $("input[type=file]").click();
  }
  getJson() {
    console.log(this.authorService.getJson(this.selectedIds));
  }
  setJson() {
    console.log(this.jsonBackUpText);
    this.authorService.setJson(this.jsonBackUpText);
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
