import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { AuthService } from '../../services/auth/auth.service';
import { PACKAGE_ROOT_URL } from '@angular/core/src/application_tokens';

@Component({
  selector: "account",
  templateUrl: './account.component.html',
  providers: [AuthService],
  styleUrls: ['../../styles/common.css', './style.css']
})
export class AccountComponent implements OnInit {


  loaded: boolean = true;
  email?: string;
  constructor(private dataService: AuthService) { }


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
