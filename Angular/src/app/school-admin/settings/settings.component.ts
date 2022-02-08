import { HttpEventType } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { MediaChange, MediaObserver } from '@angular/flex-layout';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { AlertService } from 'ngx-alerts';
import { Subscription } from 'rxjs';
import { TermLabel } from 'src/app/Models/TermLabel';
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
  dialogWidth = "40%";

  message!: string;
  progress!: number;

  session!: any;
  term = TermLabel;

  TestScheme: any = {
    Test1: 0,
    Test2: 0,
    Test3: 0,
    Exam: 0,
    SchoolID: 0
    
  }

  GradeScheme: any = {
    MinA: 0, MaxA: 0, MinB: 0, MaxB: 0, MinC: 0, MaxC: 0, MinD: 0, MaxD: 0, MinE: 0, MaxE: 0,
    MinP: 0, MaxP: 0, MinF: 0, MaxF: 0, SchoolID: 0
  }

 
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
      
    });

    this.getCurrentSession();
    this.getScoreSchemes();
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
          this.getCurrentSession();
          
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

  getCurrentSession(){
    if (this.auth.isProprietor()) {
      const s = localStorage.getItem('selectedSchool');
      if (s !== null) {
        this.admin.schoolNo.schoolID = parseInt(s)
      }
    }
    this.admin.getCurrentSession().subscribe(res=>{
      this.session = res;
      
    },
    err=> console.log(err));
    
  }

  endTerm(Id: number){
    this.admin.endTerm(Id).then(res=>{
      this.alert.success("Operation Succesful");
      this.getCurrentSession();
      console.log(res);
    },
    err=>{
      if(err.error){
        this.alert.danger(err.error.Message)
      }
      this.alert.danger("Operation failed");
      console.log(err);
    } )
    
  }

  saveTestScheme(){
    if (this.auth.isProprietor()) {
      const s = localStorage.getItem('selectedSchool');
      if (s !== null) {
        this.admin.schoolNo.schoolID = parseInt(s)
      }
    }

    this.admin.saveTestScheme(this.TestScheme).then(res=>{
      this.alert.success("Operation Succesful");
    },
    err=>{
      console.log(err);
      this.alert.danger("Operation Failed");
    });
  }

  saveGradeScheme(){
    if (this.auth.isProprietor()) {
      const s = localStorage.getItem('selectedSchool');
      if (s !== null) {
        this.admin.schoolNo.schoolID = parseInt(s)
      }
    }

    this.admin.saveGradeScheme(this.GradeScheme).then(res=>{
      this.alert.success("Operation Succesful");
    },
    err=>{
      console.log(err);
      this.alert.danger("Operation Failed");
    });
  }

  getScoreSchemes(){
    if (this.auth.isProprietor()) {
      const s = localStorage.getItem('selectedSchool');
      if (s !== null) {
        this.admin.schoolNo.schoolID = parseInt(s)
      }
    }
    this.admin.getScoreScheme().then(res=>{
      this.GradeScheme = res.Grades;
      this.TestScheme = res.Tests;
      
    },
    err=>{
      console.log(err);
    });
  }

  uploadFile(file: any){
    if (this.auth.isProprietor()) {
      const s = localStorage.getItem('selectedSchool');
      if (s !== null) {
        this.admin.schoolNo.schoolID = parseInt(s)
      }
    }
   this.admin.UploadFile(file)?.subscribe(event => {
    if(event.type === HttpEventType.UploadProgress){
      this.progress = Math.round(100 * event.loaded / event.total!);
    }
    else if(event.type === HttpEventType.Response){
      this.message = 'Upload success.';
    }
  });
  }

  compileResult(Id: number){
    this.alert.info("Please wait");
    this.admin.compileResult(Id).then(res=>{
      this.alert.success("Operation Succesful");
      this.getCurrentSession();
      console.log(res);
    },
    err=>{
      if(err.error){
        this.alert.danger(err.error.Message)
      }
      else{
        this.alert.danger("Operation failed");
      }
      console.log(err);
    } )
  }

}
