import { Component, OnInit } from '@angular/core';
import { MediaChange, MediaObserver } from '@angular/flex-layout';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { AlertService } from 'ngx-alerts';
import { Subscription } from 'rxjs';
import { AuthService } from 'src/app/resources/auth.service';
import { TitleService } from 'src/app/shared/services/title.service';
import { AddSessionsComponent } from '../add-sessions/add-sessions.component';
import { SchoolAdminService } from '../Services/school-admin.service';

@Component({
  selector: 'settings',
  templateUrl: './settings.component.html',
  styleUrls: ['./settings.component.css']
})
export class SettingsComponent implements OnInit {

  packages=["results","finance","attendance","exams"];
  unimplemented = ["games", "e-library", "e-learning"];

  mediaSub!: Subscription;
  alias!: string;
  dialogWidth = "40%"
  
  constructor(public alert: AlertService,private mediaObserver: MediaObserver, private titleService: TitleService, private auth: AuthService, private admin: SchoolAdminService, public dialog: MatDialog) { }

  ngOnInit(): void {
    this.titleService.setTitle("Settings");

    this.mediaSub = this.mediaObserver.media$.subscribe((result:MediaChange)=> {
      if(result.mqAlias !== "xs" && result.mqAlias !== "sm"){
        this.dialogWidth = "60%";
      }
      else{
        this.dialogWidth = "80%";
      }
      this.alias = result.mqAlias;
      
    }) 
  }

  openDialog(){
    const dialogConfig = new MatDialogConfig();
    dialogConfig.disableClose = true;
   dialogConfig.autoFocus = false;
    dialogConfig.width = this.dialogWidth;
    const dialogRef =  this.dialog.open(AddSessionsComponent, dialogConfig);

    dialogRef.afterClosed().subscribe(result=>{
      if(result !== null){
        if (this.auth.isProprietor()) {
          const s = localStorage.getItem('selectedSchool');
          if (s !== null) {
            this.admin.schoolNo.schoolID = parseInt(s)
          }
        }
        this.admin.addSession(result).then((res)=>{
          console.log(res);
          this.alert.success("Session Created Successfully");
          
        },
        err=>{
          console.log(err);
          if(err.error){
            this.alert.danger(err.error.Message)
          }
          else{
            this.alert.danger("Operation Failed, contact the admin for more info");
          }
          
        });
       
      }
    });
  }

}
