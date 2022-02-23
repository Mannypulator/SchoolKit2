import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Gender } from 'src/app/Models/Gender';
import { Grade } from 'src/app/Models/Grade';
import { Remarks } from 'src/app/Models/Remarks';
import { TermLabel } from 'src/app/Models/TermLabel';
import { StudentService } from '../services/student.service';

@Component({
  selector: 'app-result',
  templateUrl: './result.component.html',
  styleUrls: ['./result.component.css']
})
export class ResultComponent implements OnInit {

  constructor(private _Activatedroute: ActivatedRoute,private studService: StudentService) { }
  displayedColumns: string[] = ['position', 'name', 'weight', 'symbol'];
  dataSource = ELEMENT_DATA;

  result: any; 
  school: any;

  Grade = Grade;
  Gender = Gender;
  Remark = Remarks;
  Label = TermLabel;

  ngOnInit(): void {
    this._Activatedroute.paramMap.subscribe(params => {
      const id = params.get('id');
      this.studService.getStudentResult(id !== null ? id : "").subscribe(res=>{
        this.result = res.res;
        this.school = res.school;
       
        console.log(res);
      });
    });

  } 
}
export interface PeriodicElement {
  name: string;
  position: number;
  weight: number;
  symbol: string;
}

const ELEMENT_DATA: PeriodicElement[] = [
  { position: 1, name: 'Hydrogen', weight: 1.0079, symbol: 'H' },
  { position: 2, name: 'Helium', weight: 4.0026, symbol: 'He' },
  { position: 3, name: 'Lithium', weight: 6.941, symbol: 'Li' },
  { position: 4, name: 'Beryllium', weight: 9.0122, symbol: 'Be' },
  { position: 5, name: 'Boron', weight: 10.811, symbol: 'B' },
  { position: 6, name: 'Carbon', weight: 12.0107, symbol: 'C' },
  { position: 7, name: 'Nitrogen', weight: 14.0067, symbol: 'N' },
  { position: 8, name: 'Oxygen', weight: 15.9994, symbol: 'O' },
  { position: 9, name: 'Fluorine', weight: 18.9984, symbol: 'F' },
  { position: 10, name: 'Neon', weight: 20.1797, symbol: 'Ne' },
];