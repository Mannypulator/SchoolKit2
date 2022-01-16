import { Component, OnInit } from '@angular/core';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { AlertService } from 'ngx-alerts';
import { TermLabel } from 'src/app/Models/TermLabel';
import { AuthService } from 'src/app/resources/auth.service';
import { TitleService } from 'src/app/shared/services/title.service';
import { AddSessionsComponent } from '../add-sessions/add-sessions.component';
import { SchoolAdminService } from '../Services/school-admin.service';

@Component({
  selector: 'app-session',
  templateUrl: './session.component.html',
  styleUrls: ['./session.component.css']
})
export class SessionComponent implements OnInit {
Term = TermLabel;
ses!:any;
  constructor(public alert: AlertService, private titleService: TitleService, private auth: AuthService, private admin: SchoolAdminService, public dialog: MatDialog) { }
  sessions!: any[];
  ngOnInit(): void {
    this.titleService.setTitle("Students");

    if (this.auth.isProprietor()) {
      const s = localStorage.getItem('selectedSchool');
      if (s !== null) {
        this.admin.schoolNo.schoolID = parseInt(s)
      }
    }

    this.admin.getSessions().then(res=>{
      this.sessions = res
      
    },
    err=>{
      console.log(err);
    })
  }

  startTerm(term:any){
    let returnTerm: any = {
      TermID: term.TermID,
      SchoolID : 0
    };
    
    if (this.auth.isProprietor()) {
      const s = localStorage.getItem('selectedSchool');
      if (s !== null) {
        returnTerm.SchoolID = parseInt(s);
      }
    }
    
    console.log(returnTerm);
    this.admin.newTerm(returnTerm).then(res=>{
      this.alert.success("Successful");
    },
    err=>{
      if(err.error){
        this.alert.danger(err.error.Message);
      }
      else{
        console.log(err);
        this.alert.danger("Operation failed");
      }
    });
  }
  

}
