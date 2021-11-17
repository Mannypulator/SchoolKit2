import { Component, Inject, OnInit } from '@angular/core';
import {MatDialogRef, MAT_DIALOG_DATA} from '@angular/material/dialog';
import { ClassArm } from 'src/app/Models/ClassArm';
import { Gender } from 'src/app/Models/Gender';
import { Student } from 'src/app/Models/student';
import { AuthService } from 'src/app/resources/auth.service';
import { SchoolAdminService } from '../Services/school-admin.service';

@Component({
  selector: 'app-add-student',
  templateUrl: './add-student.component.html',
  styleUrls: ['./add-student.component.css']
})
export class AddStudentComponent implements OnInit {

  student: Student = {
    Id:"",
    FirstName: "", 
    LastName:"",
    Address:"",
    ClassArmID: 0,
    SchoolID: 0,
    Gender: Gender,
    RegNo:""
  }
  

  genderSelection = Gender;

  classes: ClassArm[] = []; 
  constructor(
    public admin: SchoolAdminService,public auth: AuthService,public dialogRef: MatDialogRef<AddStudentComponent>, @Inject(MAT_DIALOG_DATA) public data: Student
  ) { }

  ngOnInit(): void {
    this.fillClassArms();
  }

  fillClassArms(){
    if (this.auth.isProprietor()) {
      const s = localStorage.getItem('selectedSchool');
      if (s !== null) {
        this.admin.schoolNo.schoolID = parseInt(s)
      }
    }
    this.admin.getClassArms().then((res)=>{
     
      this.classes = res as unknown as ClassArm[]
    },
      (err)=>{
        console.log(err)
      });
      console.log("called");
  }

  dialogClose(){
    this.dialogRef.close(this.student);
  }

}
