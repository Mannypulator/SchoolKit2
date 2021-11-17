import { Component, OnInit } from '@angular/core';
import { AlertService } from 'ngx-alerts';
import { Gender } from 'src/app/Models/Gender';
import { AuthService } from 'src/app/resources/auth.service';
import { SchoolAdminService } from '../Services/school-admin.service';

@Component({
  selector: 'app-add-principal',
  templateUrl: './add-principal.component.html',
  styleUrls: ['./add-principal.component.css']
})
export class AddPrincipalComponent implements OnInit {

  constructor(public admin: SchoolAdminService, private alert: AlertService, private auth: AuthService) { }

  genderSelection = Gender;
  

  passwordCheck!: string;
  principalPasswordCheck!: string;

  principalMatched: boolean = true;
  matched: boolean = true;
  ngOnInit(): void {
    
  }

  onSubmitPrincipal(){
    
      if(this.admin.principal.PasswordHash == this.principalPasswordCheck ){
        this.principalMatched = true;
        const s = localStorage.getItem('selectedSchool');
        if( s !== null){
          this.admin.principal.SchoolID = parseInt(s)/////// test this immediately
          this.admin.addPrincipal().then((res)=>{
          this.alert.success("Principal Account Created Successfully");
        },
        err=>{
          console.log(err);
          this.alert.danger("Operation Failed");
        });
        }
        
      }
      else{
        this.principalMatched = false;
      }
    }

  

}
