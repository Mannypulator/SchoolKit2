import { Component, OnInit } from '@angular/core';
import { MediaChange, MediaObserver } from '@angular/flex-layout';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { AuthService } from 'src/app/resources/auth.service';
import { TitleService } from 'src/app/shared/services/title.service';
import { SchoolAdminService } from '../Services/school-admin.service';

@Component({
  selector: 'app-school-selection',
  templateUrl: './school-selection.component.html',
  styleUrls: ['./school-selection.component.css']
})
export class SchoolSelectionComponent implements OnInit {
  
  schoolIds: number[]=[1,2];
  
  constructor(private titleService: TitleService, private router: Router,private schoolAdminService: SchoolAdminService, private authService: AuthService) { }

  ngOnInit(): void {
    this.titleService.setTitle("Schools");

  }

  redirect(id: number){
    console.log("method called")
    localStorage.setItem('selectedSchool', id.toString());
    if(this.authService.redirectUrl != ""){
      this.router.navigateByUrl(this.authService.redirectUrl)
      console.log(this.authService.redirectUrl)
    }
    else{
      this.router.navigateByUrl('school-admin/principal-dashboard');
      console.log("else ran")
    }
    

  }

}
