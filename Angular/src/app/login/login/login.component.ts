import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { NgProgress } from '@ngx-progressbar/core';
import { AlertService } from 'ngx-alerts';
import { AuthService } from 'src/app/resources/auth.service';
import { ProgressbarService } from 'src/app/shared/services/progressbar.service';


@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  constructor(private authService: AuthService, 
    private alertService: AlertService, 
    public progressService: ProgressbarService, 
    private progress:NgProgress,
    private router: Router) { }

  ngOnInit(): void {
    
    this.progressService.setTitle("login");
    if (this.authService.reload){
      window.location.reload();
    }
  }

  onSubmit(form:NgForm){
    this.alertService.info('Checking login information'); 
    this.progressService.startLoading();

    const loginObserver = {
      next: (x: any) => {
        this.progressService.setSuccess();
        this.alertService.success('Welcome back ');
        this.progressService.completeLoading();
      },
      error: (err: any) => {
        this.progressService.setFailure();
        console.log(err);
        this.alertService.danger('Unable to Login');
        this.progressService.completeLoading();
      },
    };
    let model:any={
      Username : form.value.username,
      Password : form.value.password
    }

    this.authService.loginStudent(model).subscribe(loginObserver);

  }
  onSubmit2(form2: NgForm){
    this.alertService.info('Checking login information'); 
    this.progressService.startLoading();

    const loginObserver = {
      next: (x: any) => {
        this.progressService.setSuccess();
        this.alertService.success('Welcome back ');
        this.progressService.completeLoading();
        console.log('before admin if')
        if(this.authService.isAdmin()){
          console.log('after admin if')
          if(this.authService.redirectUrl != ""){
            this.router.navigateByUrl(this.authService.redirectUrl);
            console.log(this.authService.redirectUrl)
            //this.authService.redirectUrl = "";
          }
          else{
            this.router.navigateByUrl('admin');
            console.log('login else');
          }
          
        }
       
        else if(this.authService.isPrincipal() || this.authService.isProprietor()){
          console.log('after principal if');
          if(this.authService.isProprietor() && localStorage.getItem('selectedSchool') === null){
            console.log('proprietor if')
            this.router.navigateByUrl('school-admin/school-selection');
          }
          else{
            if(this.authService.redirectUrl != ""){
              this.router.navigateByUrl(this.authService.redirectUrl);
              //this.authService.redirectUrl = "";
            }
            else{
              this.router.navigateByUrl('school-admin');
            }
          }
         
        }
        else if(this.authService.isTeacher()){
          if(this.authService.redirectUrl != ""){
            this.router.navigateByUrl(this.authService.redirectUrl);
            console.log(this.authService.redirectUrl)
            //this.authService.redirectUrl = "";
          }
          else{
            this.router.navigateByUrl('teacher');
           
          }
          
        }

        //else if(this.authService.isTecher)
        //  this.router.navigateByUrl()
        //}just remember to include all these
      },
      error: (err: any) => {
        this.progressService.setFailure();
        console.log(err);
        this.alertService.danger('Unable to Login');
        this.progressService.completeLoading();
      },
    };
    let model:any={
      Username : form2.value.username,
      Password : form2.value.password
    }

    this.authService.loginStaff(model).subscribe(loginObserver);

  }

}
