import { Component, OnInit } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';
import { Arms } from 'src/app/Models/Arms';
import { ClassArm } from 'src/app/Models/ClassArm';
import { Classes } from 'src/app/Models/Classes';
import { Gender } from 'src/app/Models/Gender';
import { Teacher } from 'src/app/Models/Teacher';
import { AuthService } from 'src/app/resources/auth.service';
import { SchoolAdminService } from '../Services/school-admin.service';

@Component({
  selector: 'app-add-teacher',
  templateUrl: './add-teacher.component.html',
  styleUrls: ['./add-teacher.component.css']
})
export class AddTeacherComponent implements OnInit {

  teacher: Teacher = {
    FirstName: "",
    LastName: "",
    Address: "",
    Email: "",
    SchoolID: 0,//fix
    LgaID: 0,
    Gender: Gender,
    PasswordHash: "",
    TeacherSubjects:[]
  }

  classSubjects! :any[];

  Classes = Classes;
  Arms = Arms;


  subjectName = "";

  genderSelection = Gender;
  constructor(public admin: SchoolAdminService,public auth: AuthService,public dialogRef: MatDialogRef<AddTeacherComponent>) { }

  ngOnInit(): void {
    this.fillClassSubjects();
  }

  onSubmitTeacher(){
    this.dialogRef.close(this.teacher);
  }

  fillClassSubjects(){
    if (this.auth.isProprietor()) {
      const s = localStorage.getItem('selectedSchool');
      if (s !== null) {
        this.admin.schoolNo.schoolID = parseInt(s)
      }
    }
    this.admin.getClassSubjects().then((res)=>{
      this.classSubjects = res;
      console.log(res);
    },
    (err)=>{
      console.log(err);
    })
  }

  assignTitle(){
    this.classSubjects.forEach((sub)=>{
      if(sub.CSID == this.teacher.TeacherSubjects[0]){
        this.subjectName = sub.Title + " " + sub.CN + sub.CAN;
      }
    })
  }

}
