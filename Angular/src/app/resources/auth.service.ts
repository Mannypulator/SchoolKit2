import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { JwtHelperService } from "@auth0/angular-jwt";
import { IUser } from './IUser';
import { stringify } from '@angular/compiler/src/util';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  baseUrl: string = environment.baseUrl;
  helper = new JwtHelperService();

  currentUser : IUser = {
    userID : null,
    userRoles: []
  }

  reload=false;

 redirectUrl:string = "";

 selectedSchool: number = 0;

  constructor(private http: HttpClient, private router: Router) { }

  loginStudent(model:any){
    return this.http.post(this.baseUrl + '/api/account/studentlogin', model).pipe(
      map((response: any) => {
        const decodedToken = this.helper.decodeToken(response.token);
        this.currentUser.userID = decodedToken.UserID;
        if (typeof decodedToken.role === "string"){
          this.currentUser.userRoles.push(decodedToken.role);
        }
        else{
          this.currentUser.userRoles = decodedToken.role;
        }
       
        console.log(this.currentUser);
        localStorage.setItem('token', response.token);
      })
    );
  }

  loginStaff(model:any){
    return this.http.post(this.baseUrl + '/api/account/stafflogin', model).pipe(
      map((response: any) => {
        localStorage.setItem('token', response.token);
      })
    );
  }

  logout(){
    localStorage.removeItem('token');
    localStorage.removeItem('selectedSchool')
    this.currentUser = {
      userID: null,
      userRoles : []
    };
    this.router.navigateByUrl('login');
    this.reload = true;
    

  }

  isLoggedIn(): boolean {
    const token: string | undefined = (localStorage.getItem('token') !== null ? localStorage.getItem('token') : undefined)!;
    return !this.helper.isTokenExpired(token);
  }

  isAdmin(): boolean{
    
    if(this.isLoggedIn()){
      ///
      if(this.currentUser.userRoles.length === 0){
        this.getUser();
      }
      ///
      return this.currentUser.userRoles.includes("Admin");
    }

    return false;
  }

  getUser(){
    const token: string | undefined = (localStorage.getItem('token') !== null ? localStorage.getItem('token') : undefined)!;
    const decodedToken = this.helper.decodeToken(token);
    this.currentUser.userID = decodedToken.UserID;
    if (typeof decodedToken.role === "string"){
      this.currentUser.userRoles.push(decodedToken.role);
    }
    else{
      this.currentUser.userRoles = decodedToken.role;
    }
      
   
  }

  isPrincipal(): boolean{
    
    if(this.isLoggedIn()){
      ///
      if(this.currentUser.userRoles.length === 0){
        this.getUser();
      }
      ///
      return this.currentUser.userRoles.includes("Principal");
    }

    return false;
  }

  isProprietor(): boolean{
    
    if(this.isLoggedIn()){
      ///
      if(this.currentUser.userRoles.length === 0){
        this.getUser();
      }
      ///
      return this.currentUser.userRoles.includes("Proprietor");
    }

    return false;
  }

  isTeacher(): boolean{
    
    if(this.isLoggedIn()){
      ///
      if(this.currentUser.userRoles.length === 0){
        this.getUser();
      }
      ///
      return this.currentUser.userRoles.includes("Teacher");
    }

    return false;
  }

}
