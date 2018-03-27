import { Injectable } from '@angular/core';
import decode from 'jwt-decode';
import { HttpClient, HttpHeaders } from '@angular/common/http';

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
    return sessionStorage.getItem('token');
  }
  public isAuthenticated(): boolean {
    // get the token
    const token = this.getToken();
    return decode.tokenNotExpired(null, token);
  }

  public SignIn(loginModel: LoginModel) {
    var error = ""
    return  [this.http.post("api/signin", loginModel).subscribe((data: { token: string }) => {
      if (data.token != null || data.token != "") {
        sessionStorage.setItem('token', data.token);
        sessionStorage.setItem('email', loginModel.email);
      }
    }, (err: { error?: string }) => error = err.error),error];
  }

  public SignUp(registerModel: RegisterModel) {
    this.http.post("api/signup", registerModel).subscribe((data: { token: string }) => {
      if (data.token != null || data.token != "") {
        sessionStorage.setItem('token', data.token);
        sessionStorage.setItem('email', registerModel.email);
      }
    }, err => console.log(err));
  }

  public IsInRole(needed_role: string) {
    if (this.isAuthenticated()) {
      var _role = "";
      this.http.get("api/role").subscribe((role: string) => _role = role);
      return needed_role == _role;
    }
  }
  public GetUserName() {
    return sessionStorage.getItem("email");
  }
  public SignOut() {
    sessionStorage.removeItem("email");
    sessionStorage.removeItem("token");

  }

}
