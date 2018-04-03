import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { AccountService } from '../account.service';
import { LoginModel } from '../shared/login.model';
import { RegisterModel } from '../shared/register.model';
import { Subscription } from 'rxjs/Subscription';
import * as $ from "jquery";

@Component({
  templateUrl: './authenticate.component.html',
  providers: [AccountService],
  styleUrls: []
})
export class AuthenticateComponent {
  userLogin: LoginModel = new LoginModel();
  userRegister: RegisterModel = new RegisterModel();
  loaded: boolean = true;

  constructor(private dataService: AccountService) { }

  public SignUp() {
    if (this.userRegister.password != this.userRegister.confirmPassword)
    {
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
