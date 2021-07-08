import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { MatDrawerMode, MatSidenav } from '@angular/material/sidenav';
import { Router } from '@angular/router';

import { ProgressbarService } from '../shared/services/progressbar.service';
import { MediaObserver, MediaChange } from '@angular/flex-layout';

import { Subscription } from 'rxjs';
import { identifierModuleUrl } from '@angular/compiler';
import { AuthService } from '../resources/auth.service';


@Component({
  selector: 'app-admin',
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.css']
})
export class AdminComponent implements OnInit, OnDestroy {

  @ViewChild('sidenav') public sidenav!: MatSidenav;
  
 openState! : boolean;
 drawerMode!: MatDrawerMode;
 mediaSub!: Subscription;
 sidenv!: MatSidenav;
 alias!: string;
  constructor(private router: Router, private authService: AuthService, private mediaObserver: MediaObserver) { }

  ngOnInit(): void {
    this.mediaSub = this.mediaObserver.media$.subscribe((result:MediaChange)=> {
      if(result.mqAlias !== "xs" && result.mqAlias !== "sm"){
        this.openState = true;
        this.drawerMode = "side";
      }
      else{
        
        this.openState = false;
        this.drawerMode = "over";
      }
      this.alias = result.mqAlias;
      
    }) 
    
  }
  ngOnDestroy(): void{
    this.mediaSub.unsubscribe();
  }

  toggle():void{
    this.sidenav.toggle();
  }

  CloseSidenav():void{
    if(this.alias === "xs" || this.alias === "sm"){
      this.sidenav.close();
    }
  }



}
