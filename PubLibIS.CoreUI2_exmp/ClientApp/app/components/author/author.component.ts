import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { AuthorService } from '../../services/author.service';
import { Author } from '../../../../models/author';
import { resetFakeAsyncZone, fakeAsync } from '@angular/core/testing';

@Component({
    templateUrl: './author.component.html',
    providers: [AuthorService],
    styleUrls: ['../../styles/common.css']
})
export class AuthorComponent implements OnInit {
    author: Author = new Author();   // изменяемый товар
    authors: Author[];                // массив товаров
    loaded: boolean = true;
    constructor(private dataService: AuthorService) { }

    ngOnInit() {
        this.loadAuthors();    // загрузка данных при старте компонента  
    }
    // получаем данные через сервис
    loadAuthors() {
        this.loaded = false;
        this.dataService.getAuthorList().subscribe(authors => this.authors = authors);
        this.loaded = true;
    }
    // сохранение данных
    save() {
        if (this.author.dateOfBirth == undefined || this.author.dateOfBirth == null) {
            this.loadAuthors();
            this.cancel();
            return;
        }
        if (this.author.id == null || this.author.id == -1) {
            let newAuthor: Author = new Author(); 
            this.dataService.createAuthor(this.author).subscribe(author => newAuthor = author)
            this.authors.push(newAuthor);
        } else {
            this.dataService.updateAuthor(this.author).subscribe(data =>
                this.loadAuthors());
        }
        this.cancel();
    }

    editAuthor(a: Author) {
        this.author = a;
    }

    cancel() {
        this.author = new Author();
    }

    delete(p: Author) {
        this.dataService.deleteAuthor(p.id as number)
            .subscribe(data => this.loadAuthors());
    }

    createAuthor() {
        this.author = new Author();
        this.author.id = -1;
        console.log("11");
    }
}