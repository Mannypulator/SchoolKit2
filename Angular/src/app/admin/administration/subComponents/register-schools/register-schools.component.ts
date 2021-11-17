import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, NgForm, Validators } from '@angular/forms';
import { AlertService } from 'ngx-alerts';
import { AdminComponent } from 'src/app/admin/admin.component';
import { Gender } from 'src/app/Models/Gender';


import { School } from 'src/app/Models/School';
import { SchoolType } from 'src/app/Models/SchoolType';
import { LGA, State } from 'src/app/Models/States';
import { Subject } from 'src/app/Models/Subject';
import { AdminService } from 'src/app/resources/admin.service';



@Component({
  selector: 'app-register-schools',
  templateUrl: './register-schools.component.html',
  styleUrls: ['./register-schools.component.css']
})
export class RegisterSchoolsComponent implements OnInit {

//Note that form field names begin with small letters which should most likely throw an error if form object is used 
  
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

  genderSelection = Gender;
  

  passwordCheck!: string;
  principalPasswordCheck!: string;

  principalMatched: boolean = true;
  matched: boolean = true;
  


  constructor(private _formBuilder: FormBuilder, public admin: AdminService, private alert: AlertService) { }

  firstFormGroup!: FormGroup;
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


  onSubmitProprietor() {
    
    if(this.admin.proprietor.PasswordHash == this.passwordCheck){
      this.matched = true;
      this.admin.createProprietor().subscribe(res=>{
        this.alert.success("Proprietor Account Created Successfully");
      },
      err=>{
        console.log(err);
        this.alert.danger("Operation Failed");
      })
    }
    else{
      this.matched = false;
    }
  }

  onSubmitSchool(){
    console.log(this.admin.school.ProprietorID)
    if(this.admin.school.ProprietorID != ""){
      if(this.admin.principal.PasswordHash == this.principalPasswordCheck ){
        this.principalMatched = true;
        this.admin.createSchool().then((res)=>{
          this.alert.success("School and Principal Account Created Successfully");
        },
        err=>{
          console.log(err);
          this.alert.danger("Operation Failed");
        });
      }
      else{
        this.principalMatched = false;
      }
    }
    else{
      this.alert.danger("Register a proprietor first");
    }

  }

  checkSchoolType(){
    if(this.admin.school.Type == SchoolType.primary){
      this.sec = true;
      this.sPicked = false;
      this.admin.getPrimarySubjects().subscribe((res) =>{
        this.subjectList = res as Subject[];
      },
      err=> console.log(err))
    }
    else if(this.admin.school.Type == SchoolType.secondary){
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
      if(sub.SubjectID == this.admin.school.SsCompulsories[0]){
        this.firstCompSelectionTitle = sub.Title;
      }
    })
    
    
  }

  assignDrop(){
    this.subjectList.forEach((sub)=>{
      if(sub.SubjectID == this.admin.school.SsDrops[0]){
        this.firstDropSelectionTitle = sub.Title;
      }
    })
   
    console.log(this.firstDropSelectionTitle);
   
  }


}
