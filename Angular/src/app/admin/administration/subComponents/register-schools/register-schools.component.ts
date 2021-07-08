import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, NgForm, Validators } from '@angular/forms';
import { Gender } from 'src/app/Models/Gender';


import { School } from 'src/app/Models/School';
import { SchoolType } from 'src/app/Models/SchoolType';
import { AdminService } from 'src/app/resources/admin.service';


@Component({
  selector: 'app-register-schools',
  templateUrl: './register-schools.component.html',
  styleUrls: ['./register-schools.component.css']
})
export class RegisterSchoolsComponent implements OnInit {


  
  states: string[] = [
    'Alabama', 'Alaska', 'Arizona', 'Arkansas', 'California', 'Colorado', 'Connecticut', 'Delaware',
    'Florida', 'Georgia', 'Hawaii', 'Idaho', 'Illinois', 'Indiana', 'Iowa', 'Kansas', 'Kentucky',
    'Louisiana', 'Maine', 'Maryland', 'Massachusetts', 'Michigan', 'Minnesota', 'Mississippi',
    'Missouri', 'Montana', 'Nebraska', 'Nevada', 'New Hampshire', 'New Jersey', 'New Mexico',
    'New York', 'North Carolina', 'North Dakota', 'Ohio', 'Oklahoma', 'Oregon', 'Pennsylvania',
    'Rhode Island', 'South Carolina', 'South Dakota', 'Tennessee', 'Texas', 'Utah', 'Vermont',
    'Virginia', 'Washington', 'West Virginia', 'Wisconsin', 'Wyoming'
  ];

  ssSubjectList: string[] = ['Extra cheese', 'Mushroom', 'Onion', 'Pepperoni', 'Sausage', 'Tomato'];
  ssDropList: string[] = ['Extra cheese', 'Mushroom', 'Onion', 'Pepperoni', 'Sausage', 'Tomato'];
  schoolType!: SchoolType;
  types = SchoolType;

  constructor(private _formBuilder: FormBuilder, public admin: AdminService) { }

  firstFormGroup!: FormGroup;
  ngOnInit() {
    this.admin.proprietor = {
      Id: "",
      FirstName: "",
      LastName: "",
      Address: "",
      Email: "",
      LgaID: 0,
      gender: Gender,
      passwordHarsh: ""
    }

    this.admin.school = {
      name: " ",
      address: "",
      lgaID: 0,
      showPosition: false,
      append: "",
      proprietorID: 0,
      type: SchoolType,
      adminID: 0,
      code: 0,
      ssCompulsories: [],
      ssDrops: [],
    }
    this.admin.principal = {
      FirstName: "",
      LastName: "",
      Address: "",
      Email: "",
      SchoolID: 0,
      LgaID: 0,
      gender: Gender,
      passwordHarsh: ""
    }
  }

  getLGAs() {
    console.log("state select works");
  }

  show() {
    console.log("tt");
  }
  onSubmitProprietor() {
    this.admin.registerProprietor();
  }
}
