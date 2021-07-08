import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Principal } from '../Models/Principal';
import { Proprietor } from '../Models/Proprietor';
import { School } from '../Models/School';

@Injectable({
  providedIn: 'root'
})
export class AdminService {

  proprietor!: Proprietor;
  school!: School;
  principal!: Principal

  baseUrl: string = environment.baseUrl;
  constructor(private http: HttpClient) { }

  registerProprietor(){

  }

  subjectRange : string[] = [
      "Nursery",
      "Primary",
      "JSS",
      "SSS",
      "All"
]

  addRole(roleName: string){
    let model: any = {
      name: roleName
    }
    
    return this.http.post(this.baseUrl + '/api/admin/createRole', model);
  }

  addSubject(subject: any): Observable<any>{
    return this.http.post(this.baseUrl + '/api/subject/add', subject);
  }
}
