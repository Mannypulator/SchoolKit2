import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { AlertService } from 'ngx-alerts';

import { ClassArm } from 'src/app/Models/ClassArm';

import { SchoolAdminService } from '../Services/school-admin.service';

@Component({
  selector: 'app-assign-class',
  templateUrl: './assign-class.component.html',
  styleUrls: ['./assign-class.component.css']
})
export class AssignClassComponent implements OnInit {

  constructor(private admin: SchoolAdminService,private alert: AlertService, public dialogRef: MatDialogRef<AssignClassComponent>, @Inject(MAT_DIALOG_DATA) public data: any) { }

  classes!:ClassArm[];

  classArm:any =[];

  teacherClass: any = {
    TeacherID:"",
    ClassArmID: 0,
   
  }
  ngOnInit(): void {
    this.getClasses();
  }

  getClasses() {
    this.admin.assignSchoolID()
    this.admin.getClasses().then((res)=>{
      this.classes = res as unknown as ClassArm[];
      console.log(res);
    },
      (err)=>{
        console.log(err)
      })
  }

  SubmitClass(){
    this.teacherClass.TeacherID = this.data.Id;
    
    this.admin.assignTeacherClass(this.teacherClass).then(
      res=>{
        console.log(res);
        this.dialogRef.close(res);
        this.alert.success('Class Assigned Successfully')
      },
      err=>{
        console.log(err);
        this.alert.danger('Operation failed');
      }
    );
    
  }
}
