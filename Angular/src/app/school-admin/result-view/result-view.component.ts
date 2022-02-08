import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AlertService } from 'ngx-alerts';
import { Grade } from 'src/app/Models/Grade';
import { AuthService } from 'src/app/resources/auth.service';
import { SchoolAdminService } from '../Services/school-admin.service';

@Component({
  selector: 'app-result-view',
  templateUrl: './result-view.component.html',
  styleUrls: ['./result-view.component.css']
})
export class ResultViewComponent implements OnInit {

  constructor(private _Activatedroute: ActivatedRoute, private admin: SchoolAdminService, private auth: AuthService, private alert: AlertService, private router: Router) { }
  displayedColumns: string[] = ['subject', 'CA', 'Exam','Total','grade' ]; 
  dataSource:any = [];

  termlyResult:any;
  annualResult:any;

  PComment = "";
  APComment = "";

  Grade = Grade;

  ngOnInit(): void {
    this._Activatedroute.paramMap.subscribe(params => {
      const id = params.get('id');
      this.getResult(id !==null ? id:"");

    });
  }

  getResult(Id: string){
    if(Id !== ""){
      if (this.auth.isProprietor()) {
        const s = localStorage.getItem('selectedSchool');
        if (s !== null) {
          this.admin.schoolNo.schoolID = parseInt(s)
        }
      }
      this.admin.getStudentResult(Id).subscribe(res=>{
        this.termlyResult = res[0];
        this.annualResult = res[1];
        
        
        
        console.log(res);
      },
      err=>{
        console.log(err)
      })
    }
  }

  submitComment(){
    
    if(this.termlyResult.PrincipalComment  === ""){
      this.alert.danger('Principal\'s comment is required');
      return;
    }
    let obj = {
      PComment: this.termlyResult.PrincipalComment,
      ResultID: this.termlyResult.ResultID
    }
    this.admin.submitComment(obj).then(res=>
      {
        this.alert.success("Submitted Successfully");
        setTimeout(() =>{this.router.navigateByUrl("school-admin/student-results")},1000)
        
      })
  }

  submitAnnualComment(){
    
    if(this.annualResult.PrincipalComment  === ""){
      this.alert.danger('Principal\'s comment is required');
      return;
    }
    let obj = {
      PComment: this.termlyResult.PrincipalComment,
      ResultID: this.annualResult.ResultID
    }
    this.admin.submitComment(obj).then(res=>
      {
        this.alert.success("Submitted Successfully");
        setTimeout(() =>{this.router.navigateByUrl("school-admin/student-results")},1000)
        
      })
  }

}
