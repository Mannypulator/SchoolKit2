import { Component, OnInit } from '@angular/core';
import { TitleService } from 'src/app/shared/services/title.service';

import { ActivatedRoute } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { NgForm } from '@angular/forms';
import { AlertService } from 'ngx-alerts';

@Component({
  selector: 'app-subject',
  templateUrl: './subject.component.html',
  styleUrls: ['./subject.component.css']
})
export class SubjectComponent implements OnInit {
  baseUrl: string = environment.baseUrl;

  enrollments: any[] =[];
  searchTerm: string = "";
  
  constructor(private titleService: TitleService, private _Activatedroute:ActivatedRoute, private http: HttpClient, private alert: AlertService) { }

  ngOnInit(): void {
    this.titleService.setTitle("Subject");
    this._Activatedroute.paramMap.subscribe(params => { 
      const id = params.get('id');
      if(id != null){
        this.getStudents(parseInt(id)); 
      }
    
});
  }

  getStudents(id : number){
    this.http.get<any[]>(this.baseUrl + '/api/teacher/getEnrollments/?id='+ id).subscribe(
      res=>{
        this.enrollments = res;
        console.log(res)
      },
      err=>{
        console.log(err);
      }
    );
  }
  editEnrollment(enrollment:any){
    this.http.post<any>(this.baseUrl + '/api/enrollment/update', enrollment).subscribe(
      res=>{
        this.alert.success("Scores Successfully recorded");
        console.log(res);
      },
      err=>{
        this.alert.danger("Operation failed");
        console.log(err);
      }
    )
  }

}