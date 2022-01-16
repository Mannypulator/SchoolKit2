import { Component, OnInit } from '@angular/core';
import { TitleService } from 'src/app/shared/services/title.service';
import {MatDialog, MatDialogRef, MAT_DIALOG_DATA, MatDialogConfig} from '@angular/material/dialog';
import { ActivatedRoute } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { NgForm } from '@angular/forms';
import { AlertService } from 'ngx-alerts';
import { AuthService } from 'src/app/resources/auth.service';
import { EnrollStudentComponent } from '../enroll-student/enroll-student.component';
import { Subscription } from 'rxjs';
import { MediaChange, MediaObserver } from '@angular/flex-layout';

@Component({
  selector: 'app-subject',
  templateUrl: './subject.component.html',
  styleUrls: ['./subject.component.css']
})
export class SubjectComponent implements OnInit {
  baseUrl: string = environment.baseUrl;

  enrollments: any[] = [];
  searchTerm: string = "";
  CSID! : any;

  mediaSub!: Subscription;
 alias!: string;
 dialogWidth = "60%";

  constructor(private titleService: TitleService, private mediaObserver: MediaObserver, private auth: AuthService, public dialog: MatDialog, private _Activatedroute: ActivatedRoute, private http: HttpClient, private alert: AlertService) { }

  ngOnInit(): void {
    this.mediaSub = this.mediaObserver.media$.subscribe((result:MediaChange)=> {
      if(result.mqAlias !== "xs" && result.mqAlias !== "sm"){
        this.dialogWidth = "60%";
      }
      else{
        this.dialogWidth = "80%";
      }
      this.alias = result.mqAlias;
      
    }) 
    this._Activatedroute.paramMap.subscribe(params => {
      const id = params.get('id');
      const title = params.get('title');
      const arm = params.get('arm') ;
      if(title != null){
        this.titleService.setTitle(title + " " + arm);
      }
      else{
        this.titleService.setTitle('Subject')
      }
      
      if (id != null) {
        this.CSID = id;
        this.getStudents(parseInt(id));
      }

    });
    
   
  }

  getStudents(id: number) {
    this.http.get<any[]>(this.baseUrl + '/api/teacher/getEnrollments/?id=' + id).subscribe(
      res => {
        this.enrollments = res;
      
      },
      err => {
        console.log(err);
      }
    );
  }
  editEnrollment(enrollment: any) {
    this.http.post<any>(this.baseUrl + '/api/enrollment/update', enrollment).subscribe(
      res => {
        this.alert.success("Scores Successfully recorded");
        console.log(res);
      },
      err => {
        if(err.error){
          this.alert.danger(err.error.Message);
        }
        else{
          this.alert.danger("Operation failed");
        }
        
        console.log(err);
      }
    )
  }

  addStudent(){
    const dialogConfig = new MatDialogConfig();
    dialogConfig.disableClose = true;
    dialogConfig.autoFocus = true;
    dialogConfig.width = this.dialogWidth;
    dialogConfig.maxHeight = "500px";
    dialogConfig.data = this.CSID;
    const dialogRef = this.dialog.open(EnrollStudentComponent, dialogConfig);

    dialogRef.afterClosed().subscribe(result => {
      this.getStudents(this.CSID);
    });

  }
  unenroll(enrollment:any){
    if(confirm("Are you sure you want to uneroll this student?")){
      this.http.post<any>(this.baseUrl + '/api/teacher/deListStudent', enrollment).subscribe(
        res=>{
          this.alert.success(res.Message);
          this.getStudents(this.CSID);
        },
        err=> {
          console.log(err);
          this.alert.danger(err.Message)
        }
      )
    }
  }

}