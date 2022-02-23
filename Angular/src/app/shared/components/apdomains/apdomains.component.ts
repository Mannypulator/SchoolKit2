import { Component, Input, OnInit } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { AlertService } from 'ngx-alerts';
import { Grade } from 'src/app/Models/Grade';
import { GeneralService } from '../../services/general.service';

@Component({
  selector: 'apdomains',
  templateUrl: './apdomains.component.html',
  styleUrls: ['./apdomains.component.css']
})
export class APDomainsComponent implements OnInit {

  constructor(private general: GeneralService, private alert: AlertService) { }
  
  Grades = Grade;
  @Input() aDomain: any;
  @Input() pDomain: any;
  @Input() aDomainAnnual: any;
  @Input() pDomainAnnual: any;

  aDisplayedColumns:string[] = ["Activeness", "Attendance", "Honesty", "Neatness", "Obedience", "Punctuality", "SelfControl"];
  pDisplayedColumns = ["Creativity", "DrawingPainting", "Fluency", "Handwriting", "HandlingTools", "Sports"];
 
  ngOnInit(): void {
   
    console.log(this.aDomain);
    
  }

  gradePDomain(){
    let pDomain = {
      Creativity : this.pDomain[0].Creativity,
      DrawingPainting: this.pDomain[0].DrawingPainting,
      Fluency: this.pDomain[0].Fluency,
      Handwriting: this.pDomain[0].Handwriting,
      Sports: this.pDomain[0].Sports,
      HandlingTools: this.pDomain[0].HandlingTools,
      PsychomotorDomainID : this.pDomain[0].PsychomotorDomainID
    }
    this.general.gradeP(pDomain).then((res)=>{
      this.alert.success("Scores updated succesfully");
    },
    (err)=>{
      this.alert.danger("Operation failed");
      console.log(err);
    })
  }

  gradeADomain(){
    this.general.gradeA(this.aDomain[0]).then((res)=>{
      this.alert.success("Scores updated succesfully");
    },
    (err)=>{
      this.alert.danger("Operation failed");
      console.log(err);
    })
  }

  gradePDomainAnnual(){
    let pDomain = {
      Creativity : this.pDomainAnnual[0].Creativity,
      DrawingPainting: this.pDomainAnnual[0].DrawingPainting,
      Fluency: this.pDomainAnnual[0].Fluency,
      Handwriting: this.pDomainAnnual[0].Handwriting,
      Sports: this.pDomainAnnual[0].Sports,
      HandlingTools: this.pDomain[0].HandlingTools,
      PsychomotorDomainID : this.pDomainAnnual[0].PsychomotorDomainID
    }
    this.general.gradeP(pDomain).then((res)=>{
      this.alert.success("Scores updated succesfully");
    },
    (err)=>{
      this.alert.danger("Operation failed");
      console.log(err);
    })
  }

  gradeADomainAnnual(){
    this.general.gradeA(this.aDomainAnnual[0]).then((res)=>{
      this.alert.success("Scores updated succesfully");
    },
    (err)=>{
      this.alert.danger("Operation failed");
      console.log(err);
    })
  }

}
