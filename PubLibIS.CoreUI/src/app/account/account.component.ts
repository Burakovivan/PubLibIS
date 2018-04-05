import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { AccountService } from './account.service';
import { PACKAGE_ROOT_URL } from '@angular/core/src/application_tokens';

@Component({
  selector: "account",
  templateUrl: './account.component.html',
  providers: [AccountService],
  styleUrls: ['./shared/style.css']
})
export class AccountComponent implements OnInit {

  loaded: boolean = true;
  email?: string;

  constructor(private dataService: AccountService) { }

  ngOnInit(): void {
    this.loaded = false;
    this.email = this.dataService.GetUserName();
    this.loaded = true;
  }

  signOut() {
    this.dataService.SignOut();
    window.location.pathname = "/home";
  }
}
