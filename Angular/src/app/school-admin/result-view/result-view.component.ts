import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AlertService } from 'ngx-alerts';
import { AuthService } from 'src/app/resources/auth.service';
import { SchoolAdminService } from '../Services/school-admin.service';

export interface PeriodicElement {
  name: string;
  position: number;
  weight: number;
  symbol: string;
}

const ELEMENT_DATA: PeriodicElement[] = [
  {position: 1, name: 'Hydrogen', weight: 1.0079, symbol: 'H'},
  {position: 2, name: 'Helium', weight: 4.0026, symbol: 'He'},
  {position: 3, name: 'Lithium', weight: 6.941, symbol: 'Li'},
  {position: 4, name: 'Beryllium', weight: 9.0122, symbol: 'Be'},
  {position: 5, name: 'Boron', weight: 10.811, symbol: 'B'},
  {position: 6, name: 'Carbon', weight: 12.0107, symbol: 'C'},
  {position: 7, name: 'Nitrogen', weight: 14.0067, symbol: 'N'},
  {position: 8, name: 'Oxygen', weight: 15.9994, symbol: 'O'},
  {position: 9, name: 'Fluorine', weight: 18.9984, symbol: 'F'},
  {position: 10, name: 'Neon', weight: 20.1797, symbol: 'Ne'},
];

@Component({
  selector: 'app-result-view',
  templateUrl: './result-view.component.html',
  styleUrls: ['./result-view.component.css']
})
export class ResultViewComponent implements OnInit {

  constructor(private _Activatedroute: ActivatedRoute, private admin: SchoolAdminService, private auth: AuthService, private alert: AlertService, private router: Router) { }
  displayedColumns: string[] = ['subject', 'CA', 'Exam','Total','grade' ]; 
  dataSource:any = [];

  termlyResult:any = [];
  annualResult:any = [];

  PComment = "";

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
      this.admin.getStudentResult(Id).then(res=>{
        this.termlyResult = res[0];
        this.annualResult = res[1];
        this.dataSource = res[0].Enrollments;
        console.log(res);
      },
      err=>{
        console.log(err)
      })
    }
  }

  submitComment(){
    
    if(this.PComment === ""){
      this.alert.danger('Principal\'s comment is required');
      return;
    }
    let obj = {
      PComment: this.PComment,
      ResultID: this.termlyResult.ResultID
    }
    this.admin.submitComment(obj).then(res=>
      {
        this.alert.success("Submitted Successfully");
        setTimeout(() =>{this.router.navigateByUrl("school-admin/student-results")},1000)
        
      })
  }

}
