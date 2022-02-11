import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { AlertService } from 'ngx-alerts';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-student-results',
  templateUrl: './student-results.component.html',
  styleUrls: ['./student-results.component.css']
})
export class StudentResultsComponent implements OnInit {

  constructor(private http: HttpClient, private alert: AlertService) { }

  baseUrl: string = environment.baseUrl;
  students: any[]= [];
  ngOnInit(): void {
    this.getStudents();
  }

  getStudents(){
    
    this.http.get<any>(this.baseUrl + '/api/term/formTeachResList').subscribe(
      res=>{
       this.students = res;
      },
      err=> {console.log(err);
      }
    );
  }

}
