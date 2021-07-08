import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';

import { NgProgressModule } from '@ngx-progressbar/core';


import { LoginRoutingModule } from './login-routing.module';
import { LoginComponent } from './login/login.component';
import { SharedModuleModule } from '../shared/shared-module/shared-module.module';


@NgModule({
  declarations: [LoginComponent],
  imports: [
    
    NgProgressModule,
    CommonModule,
    FormsModule,
    HttpClientModule,
    LoginRoutingModule,
    SharedModuleModule 
    
  ]
})
export class LoginModule { }
