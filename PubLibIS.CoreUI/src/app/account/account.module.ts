import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AccountService } from './account.service';
import { AccountComponent } from './account.component';

@NgModule({

  imports: [CommonModule],
  declarations: [
    AccountComponent
  ],
  providers: [AccountService],
  exports: [AccountComponent],
  bootstrap: [AccountComponent]
})
export class AccountModule { }
