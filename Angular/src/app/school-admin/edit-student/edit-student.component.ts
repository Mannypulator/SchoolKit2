import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ClassArm } from 'src/app/Models/ClassArm';
import { Gender } from 'src/app/Models/Gender';
import { Student } from 'src/app/Models/student';
import { AuthService } from 'src/app/resources/auth.service';
import { AddStudentComponent } from '../add-student/add-student.component';
import { SchoolAdminService } from '../Services/school-admin.service';

@Component({
  selector: 'app-edit-student',
  templateUrl: './edit-student.component.html',
  styleUrls: ['./edit-student.component.css']
})
export class EditStudentComponent implements OnInit {

  constructor(public admin: SchoolAdminService,public auth: AuthService,public dialogRef: MatDialogRef<EditStudentComponent>, @Inject(MAT_DIALOG_DATA) public data: any) { }
  genderSelection = Gender;

  classes: ClassArm[] = [];

  student = this.data;

  editedStudent: Student = {
    Id:"",
    FirstName: "", 
    LastName:"",
    Address:"",
    ClassArmID: 0,
    SchoolID: 0,
    Gender: Gender,
    RegNo:""
  }

  ngOnInit(): void {
    this.fillClassArms();
    console.log(this.student)
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
    this.editedStudent.FirstName = this.student.FirstName;
    this.editedStudent.LastName = this.student.LastName;
    this.editedStudent.Address = this.student.Address;
    this.editedStudent.ClassArmID = this.student.ClassArmID;
    this.editedStudent.Gender = this.student.Gender;
    this.editedStudent.SchoolID = this.student.SchoolID;
    this.editedStudent.RegNo = this.student.RegNo;
    this.editedStudent.Id = this.student.Id;

    this.dialogRef.close(this.editedStudent);
  }

}
