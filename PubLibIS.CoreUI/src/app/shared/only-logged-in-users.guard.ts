import { CanActivate } from "@angular/router";
import { Injectable } from "@angular/core";
import { AccountService } from "../account/account.service";

@Injectable()
export class OnlyLoggedInUsersGuard implements CanActivate {
  constructor(private userService: AccountService) { };

  canActivate() {
    console.log("OnlyLoggedInUsers");
    if (this.userService.isAuthenticated()) {
      return true;
    } else {
      window.alert("You don't have permission to view this page");
      return false;
    }
  }
}
