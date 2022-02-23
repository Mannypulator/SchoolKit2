import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AlertService } from 'ngx-alerts';
import { Grade } from 'src/app/Models/Grade';
import { AuthService } from 'src/app/resources/auth.service';
import { GeneralService } from '../../services/general.service';


@Component({
  selector: 'app-result-view',
  templateUrl: './result-view.component.html',
  styleUrls: ['./result-view.component.css']
})
export class ResultViewComponent implements OnInit {

  constructor(private _Activatedroute: ActivatedRoute, private general: GeneralService, public auth: AuthService, private alert: AlertService, private router: Router) { }
  aDisplayedColumns = ["Activeness", "Attendance", "Honesty", "Neatness", "Obedience", "Punctuality", "SelfControl"];

  

  termlyResult: any;
  annualResult: any;

  aDomain :any=[];

  PComment = "";
  APComment = "";

  Grade = Grade;

  ngOnInit(): void {
    this._Activatedroute.paramMap.subscribe(params => {
      const id = params.get('id');
      this.getResult(id !== null ? id : "");

    });
    
  }

  getResult(Id: string) {

    this.general.getStudentResult(Id).subscribe(res => {
      this.termlyResult = res[0];
      this.annualResult = res[1];
      console.log(res);
     
    },
      err => {
        console.log(err)
      })

  }

  submitPComment() {

     if (this.termlyResult.PrincipalComment === "") {
       this.alert.danger('Principal\'s comment is required');
       return;
     }
     let obj = {
       Comment: this.termlyResult.PrincipalComment,
       ResultID: this.termlyResult.ResultID
     }
     this.general.submitComment(obj).then(res => {
       this.alert.success("Submitted Successfully");
       if(!this.annualResult){
        setTimeout(() => { this.router.navigateByUrl("school-admin/student-results") }, 1000)
      }
 
     })
  }

  submitAnnualComment() {

    if (this.annualResult.PrincipalComment === "") {
      this.alert.danger('Principal\'s comment is required');
      return;
    }
    let obj = {
      Comment: this.termlyResult.PrincipalComment,
      ResultID: this.annualResult.ResultID
    }
    this.general.submitComment(obj).then(res => {
      this.alert.success("Submitted Successfully");
      setTimeout(() => { this.router.navigateByUrl("school-admin/student-results") }, 1000)

    })
  }

  submitTComment() {

    if (this.termlyResult.TeacherComment === "") {
      this.alert.danger('Teacher\'s comment is required');
      return;
    }
    let obj = {
      Comment: this.termlyResult.TeacherComment,
      ResultID: this.termlyResult.ResultID
    }
    this.general.submitTComment(obj).then(res => {
      this.alert.success("Submitted Successfully");
      if(!this.annualResult){
        setTimeout(() => { this.router.navigateByUrl("teacher/student-result") }, 1000)
      }
      
      

    })
 }

 submitAnnualTComment() {

   if (this.annualResult.TeacherComment === "") {
     this.alert.danger('Teacher\'s comment is required');
     return;
   }
   let obj = {
     Comment: this.termlyResult.TeacherComment,
     ResultID: this.annualResult.ResultID
   }
   this.general.submitTComment(obj).then(res => {
     this.alert.success("Submitted Successfully");
     setTimeout(() => { this.router.navigateByUrl("teacher/student-result") }, 1000)

   })
 }

 
}
