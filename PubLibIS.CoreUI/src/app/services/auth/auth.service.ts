import { Injectable } from '@angular/core';
import decode from 'jwt-decode';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import * as $ from "jquery";

import { LoginModel } from '../../models/login';
import { RegisterModel } from '../../models/register';
import { Subscription } from 'rxjs/Subscription';

@Injectable()
export class AuthService {
  http: HttpClient
  constructor(http: HttpClient) {
    this.http = http;
  }

  public getToken(): string {
    return localStorage.getItem('token');
  }
  public isAuthenticated(): boolean {
    // get the token
    const token = this.getToken();
    return decode.tokenNotExpired(null, token);
  }

  public SignIn(loginModel: LoginModel, errorElement : string) {
    var error = ""
    this.http.post("api/signin", loginModel).subscribe((data: { token: string }) => {
      if (data.token != null || data.token != "") {
        localStorage.setItem('token', data.token);
        localStorage.setItem('email', loginModel.email);
        window.location.pathname = "/home";
      }
    }, (resp: { error?: { message?: string } }) => { $(errorElement).text(resp.error.message) });
  }

  public SignUp(registerModel: RegisterModel, errorElement:string) {
    this.http.post("api/signup", registerModel).subscribe((data: { token: string }) => {
      if (data.token != null || data.token != "") {
        localStorage.setItem('token', data.token);
        localStorage.setItem('email', registerModel.email);
        window.location.pathname = "/home";
      }
    }, (resp: { error?: { message?: string } }) => { $(errorElement).text(resp.error.message) });
  }

  public IsInRole(needed_role: string) {
    if (this.isAuthenticated()) {
      var _role = "";
      this.http.get("api/role").subscribe((role: string) => _role = role);
      return needed_role == _role;
    }
  }
  public GetUserName() {
    return localStorage.getItem("email");
  }
  public SignOut() {
    localStorage.removeItem("email");
    localStorage.removeItem("token");

  }

}
