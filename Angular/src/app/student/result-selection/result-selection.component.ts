import { Component, OnInit } from '@angular/core';
import { Arms } from 'src/app/Models/Arms';
import { Classes } from 'src/app/Models/Classes';
import { ResultType } from 'src/app/Models/ResultType';
import { TermLabel } from 'src/app/Models/TermLabel';
import { StudentService } from '../services/student.service';

@Component({
  selector: 'app-result-selection',
  templateUrl: './result-selection.component.html',
  styleUrls: ['./result-selection.component.css']
})
export class ResultSelectionComponent implements OnInit {

  constructor(private studService: StudentService) { }

  resultList :any[] = [];

  Class = Classes;
  Arm = Arms;
  Label = TermLabel;
  Type = ResultType;

  ngOnInit(): void {
    this.studService.getResultList().then(res=>{
      this.resultList = res;
      console.log(res);
    },
    err=> console.log(err))
  }

}
