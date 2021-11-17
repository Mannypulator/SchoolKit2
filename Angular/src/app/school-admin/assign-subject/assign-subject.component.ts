import { AstMemoryEfficientTransformer } from '@angular/compiler';
import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef,MAT_DIALOG_DATA } from '@angular/material/dialog';
import { AlertService } from 'ngx-alerts';
import { Arms } from 'src/app/Models/Arms';
import { Classes } from 'src/app/Models/Classes';
import { Subject } from 'src/app/Models/Subject';
import { AuthService } from 'src/app/resources/auth.service';
import { SchoolAdminService } from '../Services/school-admin.service';

@Component({
  selector: 'app-assign-subject',
  templateUrl: './assign-subject.component.html',
  styleUrls: ['./assign-subject.component.css']
})
export class AssignSubjectComponent implements OnInit {

  constructor(public admin: SchoolAdminService,private alert: AlertService,public auth: AuthService,public dialogRef: MatDialogRef<AssignSubjectComponent>, @Inject(MAT_DIALOG_DATA) public data: any) { }

  SubjectID: number = 0;
  subjects : Subject[] = [];
  classSubjects: any[] = [];

  selectedCS:any ={
    TeacherID : "",
    ClassSubjectIds : []
  }

  Classes = Classes;
  Arm = Arms;
  ngOnInit(): void {
    this.GetSubjects();
  }

  GetSubjects(){
    if (this.auth.isProprietor()) {
      const s = localStorage.getItem('selectedSchool');
      if (s !== null) {
        this.admin.schoolNo.schoolID = parseInt(s)
      }
    }
    this.admin.getSubjects().then((res)=>{
      this.subjects = res;
      console.log(res);
    },
    (err)=>{
      console.log(err);
    })
  }

  SubjectClassses(){
    if (this.auth.isProprietor()) {
      const s = localStorage.getItem('selectedSchool');
      if (s !== null) {
        this.admin.schoolNo.schoolID = parseInt(s)
      }
    }
    this.admin.getSubjectClasses(this.SubjectID).then((res)=>{
      this.classSubjects = res;
      console.log(res);
    },
    (err)=>{
      console.log(err);
    })
  }

  SubmitCS(){
    this.classSubjects.forEach(x=>{
      if(x.Selected == true){
        this.selectedCS.ClassSubjectIds.push(x.ClassSubjectID);
      }
      //then call a method to submit it along with teacher id
      //pass teacher id through dialog data
    });
    this.selectedCS.TeacherID = this.data.Id;
      
      this.admin.addTeacherSubject(this.selectedCS).then((res)=>{
        this.alert.success("Subject Assigned Successfully");
        var f: any ={
          TeacherID: this.data.Id,
          TeacherSubjects : res
        }
        this.dialogRef.close(f);
      },
      err=>{
        this.alert.danger("failed to assign subject");
        console.log(err);
      })
  }
}
