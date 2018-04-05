import { Component, OnInit } from '@angular/core';
import { AccountService } from '../account.service';
import { LoginModel } from '../shared/login.model';
import { RegisterModel } from '../shared/register.model';
import * as $ from "jquery";

@Component({
  templateUrl: './authenticate.component.html',
  providers: [AccountService],
})
export class AuthenticateComponent implements OnInit {

  ngOnInit(): void {
    console.log("hello from authenticate");
  }
  userLogin: LoginModel = new LoginModel();
  userRegister: RegisterModel = new RegisterModel();
  loaded: boolean = true;

  constructor(private dataService: AccountService) { }

  public SignUp() {
    if (this.userRegister.password != this.userRegister.confirmPassword) {
      $("#up.error-message").text("Passwords are different");
      return;
    }

    if (this.userRegister != null) {
      this.dataService.SignUp(this.userRegister, "#up.error-message");
    }
  }

  public SignIn() {
    if (this.userLogin != null) {
      this.dataService.SignIn(this.userLogin, "#in.error-message");
    }
  }


}
