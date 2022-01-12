import { Component, OnInit, ViewChild } from '@angular/core';
import { MediaChange, MediaObserver } from '@angular/flex-layout';
import { MatDrawerMode, MatSidenav } from '@angular/material/sidenav';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { AuthService } from 'src/app/resources/auth.service';

@Component({
  selector: 'app-student',
  templateUrl: './student.component.html',
  styleUrls: ['./student.component.css']
})
export class StudentComponent implements OnInit {

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
