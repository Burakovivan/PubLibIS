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

    if (this.userRegister != null && this.userRegister.password == this.userRegister.confirmPassword) {
      console.log(this.userRegister);
      this.dataService.SignUp(this.userRegister);
    }
  }

  public SignIn() {
    if (this.userLogin != null) {
      var result = this.dataService.SignIn(this.userLogin);
      (result[0] as Subscription).add(() => {
        if (this.dataService.isAuthenticated()) {
          window.location.pathname = "/home";
        }
      });
      if (result[0].toString() != "") {
        $(".error-message").val(result[0].toString());
      }
    }
  }


}
