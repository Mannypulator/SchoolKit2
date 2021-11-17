import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Arms } from 'src/app/Models/Arms';
import { Classes } from 'src/app/Models/Classes';
import { Gender } from 'src/app/Models/Gender';
import { ReturnTeacher, Teacher } from 'src/app/Models/Teacher';
import { TitleService } from 'src/app/shared/services/title.service';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {
  baseUrl: string = environment.baseUrl;

  Gender = Gender;
  Class = Classes;
  Arm = Arms;
  

  teacher: ReturnTeacher ={
     Id: "",
     FirstName : "",
     LastName: "",
     Eamil: "",
     SchoolID: 0,
     Gender : 0,
     PhoneNumber: "",
     TeacherSubjects: [],
  }

  

  constructor(private titleService: TitleService, private http: HttpClient) { }

  ngOnInit(): void {
    this.titleService.setTitle("Home");
    this.getTeacher();
  }

  getTeacher(){
    this.http.get(this.baseUrl + '/api/teacher/getTeacher').subscribe(res=>{
      this.teacher=res as ReturnTeacher;
    },
    err=>{
      console.log(err);
    });
  }

}
