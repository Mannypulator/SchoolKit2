import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { ClassArm } from 'src/app/Models/ClassArm';
import { AuthService } from 'src/app/resources/auth.service';
import { TitleService } from 'src/app/shared/services/title.service';
import { SchoolAdminService } from '../Services/school-admin.service';
import {MatDialog, MatDialogRef, MAT_DIALOG_DATA, MatDialogConfig} from '@angular/material/dialog';
import { AddStudentComponent } from '../add-student/add-student.component';
import { Student } from 'src/app/Models/student';
import { AlertService } from 'ngx-alerts';
import { Gender } from 'src/app/Models/Gender';

import { Classes } from 'src/app/Models/Classes';
import { Arms } from 'src/app/Models/Arms';
import { EditStudentComponent } from '../edit-student/edit-student.component';

@Component({
  selector: 'student-results',
  templateUrl: './student-results.component.html',
  styleUrls: ['./student-results.component.css'], 

})
export class StudentResultsComponent implements OnInit {

 
  students: any[] = [];
  Gender = Gender;

  studentSearch: string = "";
  Classes = Classes;
  Arms = Arms;


  constructor(public alert: AlertService, private titleService: TitleService, private auth: AuthService, private admin: SchoolAdminService, public dialog: MatDialog) { }

  ngOnInit(): void {
    this.titleService.setTitle("Students");
    
    this.getStudents();
  }

  getStudents(){
    if (this.auth.isProprietor()) {
      const s = localStorage.getItem('selectedSchool');
      if (s !== null) {
        this.admin.schoolNo.schoolID = parseInt(s)
      }
    }
    this.admin.getRStudent().then((res)=>{
      this.students = res;
      console.log(res);
    },
    (err)=>{
      console.log(err);
    })
  }

  filter(CAI:number){
    if (this.auth.isProprietor()) {
      const s = localStorage.getItem('selectedSchool');
      if (s !== null) {
        this.admin.schoolNo.schoolID = parseInt(s)
      }
    }
    this.admin.filterRStudent(CAI).then((res)=>{
      this.students = res;
    },
    (err)=>{
      console.log(err);
    })
  }

  findStudent(){
    if (this.auth.isProprietor()) {
      const s = localStorage.getItem('selectedSchool');
      if (s !== null) {
        this.admin.schoolNo.schoolID = parseInt(s)
      }
    }
    this.admin.findStudent(this.studentSearch).then((res)=>{
      this.students = res;
    },
    (err)=>{
      console.log(err);
    })
  }

   
}
