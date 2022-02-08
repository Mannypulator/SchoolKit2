import { Component, OnInit, Input } from '@angular/core';
import { Grade } from 'src/app/Models/Grade';

@Component({
  selector: 'result-table',
  templateUrl: './result-table.component.html',
  styleUrls: ['./result-table.component.css']
})
export class ResultTableComponent implements OnInit {

  constructor() { }
  displayedColumns: string[] = ['subject', 'CA', 'Exam','Total','grade' ]; 
  annualDisplayedColumns: string[] = ['subject', 'firstTerm', 'secondTerm', 'thirdTerm', 'grade'];
 



  PComment = "";

  Grade = Grade;

  @Input() termlyResult:any;
  @Input() annualResult:any;
  ngOnInit(): void {
   console.log(this.annualResult)
  }

}
