import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';


import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { PageNotFoundComponent } from './shared/components/page-not-found/page-not-found.component';
import { AuthInterceptor } from './resources/auth.interceptor';

import { ProgressbarService } from './shared/services/progressbar.service';
import { TitleService } from './shared/services/title.service';
import { AdminService } from './resources/admin.service';

import { AuthService } from './resources/auth.service';














@NgModule({
  declarations: [
    AppComponent,
    PageNotFoundComponent,
    
    
    
   
   
    
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    HttpClientModule,
   
    
  ],
  providers: [
    {provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true},
    AdminService,
    ProgressbarService,
    TitleService, 
    
    AuthService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
