import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Observable} from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { Gender } from '../Models/Gender';
import { Principal } from '../Models/Principal';
import { Proprietor } from '../Models/Proprietor';
import { School, SchoolModel } from '../Models/School';
import { SchoolType } from '../Models/SchoolType';


@Injectable({
  providedIn: 'root'
})
export class AdminService {

  proprietor: Proprietor= {
    Id: "",
    FirstName: "",
    LastName: "",
    Address: "",
    Email: "",
    LgaID: 0,
    Gender: Gender,
    PasswordHash: ""
  };

  school: School = {
    SchoolID: 0,
    Name: "",
    Address: "",
    LgaID: 0,
    ShowPosition: false,
    Append: "",
    ProprietorID: this.proprietor.Id,
    Type: SchoolType.None,
    SsCompulsories: [],
    SsDrops: [],
  };

  principal: Principal = {
    FirstName: "",
    LastName: "",
    Address: "",
    Email: "",
    SchoolID: this.school.SchoolID,
    LgaID: 0,
    Gender: Gender,
    PasswordHash: ""
  }


  baseUrl: string = environment.baseUrl;
  constructor(private http: HttpClient) { }

  registerProprietor(){}



  addRole(roleName: string){
    let model: any = {
      name: roleName
    }
    
    return this.http.post(this.baseUrl + '/api/admin/createRole', model);
  }

  addSubject(subject: any): Observable<any>{
    return this.http.post(this.baseUrl + '/api/subject/add', subject);
  }

  getPrimarySubjects(){
    return this.http.get(this.baseUrl + '/api/subject/getPrimary');
  }

  getSecondarySubjects(){
    return this.http.get(this.baseUrl + '/api/subject/getSecondary');
  }

  getStates(){
    return this.http.get(this.baseUrl + '/api/account/getStates').toPromise();
  }

  getLGAs(state: number){
    let stateId: any = {
      StateID: state
    }
    return this.http.post(this.baseUrl + '/api/account/getLgas', stateId).toPromise();
  }

  createProprietor(){
    let returnProprietor = new Proprietor;
    
    return this.http.post(this.baseUrl + '/api/admin/addProprietor', this.proprietor).pipe(map((response)=>
    {
      returnProprietor = response as Proprietor;
      this.school.ProprietorID = returnProprietor.Id;
     
    }));
  }

  createSchool(){
    let schoolModel: SchoolModel = {
      school : this.school,
      principal : this.principal
    }
    return this.http.post(this.baseUrl + '/api/admin/createSchool', schoolModel).toPromise();
  }
  pCreateSchool(){
    return this.http.post(this.baseUrl + '/api/admin/createSchool', this.school).toPromise();
  }
}
