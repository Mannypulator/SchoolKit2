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
  selector: 'app-students',
  templateUrl: './students.component.html',
  styleUrls: ['./students.component.css'],

})
export class StudentsComponent implements OnInit {

 
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
    this.admin.getStudent().then((res)=>{
      this.students = res;
    },
    (err)=>{
      console.log(err);
    })
  }

 

  openDialog(){
    const dialogConfig = new MatDialogConfig();
    dialogConfig.disableClose = true;
    dialogConfig.autoFocus = true;
    dialogConfig.width = "60%";
    const dialogRef =  this.dialog.open(AddStudentComponent, dialogConfig);

    dialogRef.afterClosed().subscribe(result => {
      if(result !== null){
        if (this.auth.isProprietor()) {
          const s = localStorage.getItem('selectedSchool');
          if (s !== null) {
            this.admin.schoolNo.schoolID = parseInt(s)
          }
        }
        this.admin.addStudent(result as Student).then((res)=>{
          console.log(res);
          this.alert.success("Student Account Created Successfully");
          this.getStudents();
        },
        err=>{
          console.log(err);
          this.alert.danger("Operation Failed");
        });
       
      }
      
      
    });
  }

  editStudent(student: any){
    const dialogConfig = new MatDialogConfig();
    dialogConfig.disableClose = true;
    dialogConfig.autoFocus = true;
    dialogConfig.width = "60%";
    dialogConfig.data = Object.assign({},student);
    const dialogRef =  this.dialog.open(EditStudentComponent, dialogConfig);

    dialogRef.afterClosed().subscribe(result => {
      if(result !== null){
        if (this.auth.isProprietor()) {
          const s = localStorage.getItem('selectedSchool');
          if (s !== null) {
            this.admin.schoolNo.schoolID = parseInt(s)
          }
        }
        this.admin.EditStudent(result as Student).then((res)=>{
          console.log(res);
          this.alert.success("Student Account Edited Successfully");
          this.getStudents();
        },
        err=>{
          console.log(err);
          this.alert.danger("Operation Failed");
        });
        console.log(result as Student);
      }
      
      
    });
  }

  filter(CAI:number){
    if (this.auth.isProprietor()) {
      const s = localStorage.getItem('selectedSchool');
      if (s !== null) {
        this.admin.schoolNo.schoolID = parseInt(s)
      }
    }
    this.admin.filterStudent(CAI).then((res)=>{
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

  removeStudent(Id: string){
    if(confirm("Are you sure you want to delete student account?")){
      this.admin.deleteStudent(Id).then(res=>{
        this.alert.success("Student account deleted succesfully");
        this.getStudents();
      },
      err=>{
        console.log(err);
      })
    }
  }

}
