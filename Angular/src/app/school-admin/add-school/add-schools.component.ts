import { Component, OnInit } from '@angular/core';
import { AlertService } from 'ngx-alerts';

import { SchoolType } from 'src/app/Models/SchoolType';
import { LGA, State } from 'src/app/Models/States';
import { Subject } from 'src/app/Models/Subject';
import { AdminService } from 'src/app/resources/admin.service';
import { SchoolAdminService } from '../Services/school-admin.service';

@Component({
  selector: 'app-schools',
  templateUrl: './add-schools.component.html',
  styleUrls: ['./add-schools.component.css']
})
export class AddSchoolsComponent implements OnInit {
  states: State[] = [];
  lgas: LGA[] = [];

  subjectList: Subject[] = [];
 
  schoolType = SchoolType;

  firstCompSelection!: Subject;
  firstDropSelection!: Subject;

  firstDropSelectionTitle!: string;
  firstCompSelectionTitle!: string

  sPicked: boolean = true;
  sec: boolean = true;

  selectedState!: number


  constructor(public admin: AdminService, public sAdmin: SchoolAdminService, private alert: AlertService) { }

  ngOnInit() {
    
    this.admin.getStates().then((res)=>
    {
      this.states = res as State[];
    })
   }
 
   getLGAs() {
     
     this.admin.getLGAs(this.selectedState).then((res)=>
     {
       this.lgas = res as LGA[];
       console.log(this.lgas);
     })
   }
 
   onSubmitSchool(){
         this.admin.createSchool().then((res)=>{
           this.alert.success("School Created Successfully");//////////////
         },
         err=>{
           console.log(err);
           this.alert.danger("Operation Failed");
         });
       }
 
   
 
   checkSchoolType(){
     if(this.sAdmin.school.Type == SchoolType.primary){
       this.sec = true;
       this.sPicked = false;
       this.admin.getPrimarySubjects().subscribe((res) =>{
         this.subjectList = res as Subject[];
       },
       err=> console.log(err))
     }
     else if(this.sAdmin.school.Type == SchoolType.secondary){
       this.sec = false;
       this.sPicked = false;
       this.admin.getSecondarySubjects().subscribe((res) =>{
         this.subjectList = res as Subject[];
       },
       err => console.log(err))
     }
   }
 
   assignComp(){
     this.subjectList.forEach((sub)=>{
       if(sub.SubjectID == this.sAdmin.school.SsCompulsories[0]){
         this.firstCompSelectionTitle = sub.Title;
       }
     })
     
     
   }
 
   assignDrop(){
     this.subjectList.forEach((sub)=>{
       if(sub.SubjectID == this.sAdmin.school.SsDrops[0]){
         this.firstDropSelectionTitle = sub.Title;
       }
     })
    
     console.log(this.firstDropSelectionTitle);
    
   }
 

}
