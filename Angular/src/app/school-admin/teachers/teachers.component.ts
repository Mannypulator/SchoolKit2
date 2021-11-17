import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import {MatDialog, MatDialogRef, MAT_DIALOG_DATA, MatDialogConfig} from '@angular/material/dialog';
import { AlertService } from 'ngx-alerts';
import { Arms } from 'src/app/Models/Arms';
import { Classes } from 'src/app/Models/Classes';
import { Gender } from 'src/app/Models/Gender';
import { ReturnTeacher, Teacher } from 'src/app/Models/Teacher';
import { AuthService } from 'src/app/resources/auth.service';
import { TitleService } from 'src/app/shared/services/title.service';
import { AddTeacherComponent } from '../add-teacher/add-teacher.component';
import { AssignSubjectComponent } from '../assign-subject/assign-subject.component';
import { SchoolAdminService } from '../Services/school-admin.service';

@Component({
  selector: 'app-teachers',
  templateUrl: './teachers.component.html',
  styleUrls: ['./teachers.component.css'],
  
})


export class TeachersComponent implements OnInit {

  teachers: any[]= [];
  Gender = Gender;
  Arm = Arms;
  Classes =  Classes;

  teacherSearch: string = "";
  constructor(private titleService: TitleService,private alert: AlertService, private auth: AuthService, private admin: SchoolAdminService, public dialog: MatDialog) { }

  ngOnInit(): void {
    this.titleService.setTitle("Teachers");
    this.getTeachers();
  }

  getTeachers(){
    if (this.auth.isProprietor()) {
      const s = localStorage.getItem('selectedSchool');
      if (s !== null) {
        this.admin.schoolNo.schoolID = parseInt(s)
      }
    }
    this.admin.getTeachers().then((res)=>{
      this.teachers = res;
      console.log(res)
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
    const dialogRef =  this.dialog.open(AddTeacherComponent, dialogConfig);

    dialogRef.afterClosed().subscribe(result => {
      if(result !== null){
        if (this.auth.isProprietor()) {
          const s = localStorage.getItem('selectedSchool');
          if (s !== null) {
            this.admin.schoolNo.schoolID = parseInt(s)
          }
        }
        this.admin.addTeacher(result).then((res)=>{
          console.log(res);
          this.alert.success("Teacher Account Created Successfully");
          this.getTeachers();
        },
        err=>{
          console.log(err);
          this.alert.danger("Operation Failed");
        });
        console.log(result as Teacher);
      }
      
      
    });
  }

  openDialog2(teacher:any){
    const dialogConfig = new MatDialogConfig();
    dialogConfig.disableClose = true;
    dialogConfig.autoFocus = true;
    dialogConfig.width = "60%";
    dialogConfig.data = {Id: teacher.Id}

    const dialogRef =  this.dialog.open(AssignSubjectComponent, dialogConfig);

    dialogRef.afterClosed().subscribe(result => {
      if(result !== null){
       var teacherSubject = this.teachers.find(x=>x.Id == result.TeacherID);
       teacherSubject.TeacherSubjects = result.TeacherSubjects;
       console.log(teacherSubject);
      }
      
      
    });
  }
  
  findTeacher(){
    if (this.auth.isProprietor()) {
      const s = localStorage.getItem('selectedSchool');
      if (s !== null) {
        this.admin.schoolNo.schoolID = parseInt(s)
      }
    }
    this.admin.findTeachers(this.teacherSearch).then((res)=>{
      this.teachers = res;
    },
    (err)=>{
      console.log(err);
    })
  }

  DissociateSubject(id:number, j:number, i: number){
    if(confirm("Are you sure you want to dissociate teacher from this subject?")){
      this.admin.dissociateSubject(id).then(res=>{
        this.alert.success("Subjected successfully dissociated from teacher");
        this.teachers[i].TeacherSubjects.splice(j,1);
      },
      err=>{
        console.log(err);
      })
    }
   
  }

  RemoveTeacher(Id: string){
    if(confirm("Are you sure you want to delete teacher account?")){
      this.admin.deleteTeacher(Id).then(res=>{
        this.alert.success("Teacher account deleted succesfully");
        this.getTeachers();
      },
      err=>{
        console.log(err);
      })
    }
  }
}
