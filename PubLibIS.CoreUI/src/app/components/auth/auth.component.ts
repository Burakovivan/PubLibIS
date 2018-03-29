import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { AuthService } from '../../services/auth/auth.service';
import { LoginModel } from '../../models/login';
import { RegisterModel } from '../../models/register';
import { Subscription } from 'rxjs/Subscription';
import * as $ from "jquery";

@Component({
  templateUrl: './auth.component.html',
  providers: [AuthService],
  styleUrls: ['../../styles/common.css']
})
export class AuthComponent {
  userLogin: LoginModel = new LoginModel();
  userRegister: RegisterModel = new RegisterModel();
  loaded: boolean = true;

  constructor(private dataService: AuthService) { }

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
