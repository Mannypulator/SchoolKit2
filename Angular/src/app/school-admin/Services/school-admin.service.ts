import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Gender } from 'src/app/Models/Gender';
import { Principal } from 'src/app/Models/Principal';
import { School } from 'src/app/Models/School';
import { SchoolType } from 'src/app/Models/SchoolType';
import { Student } from 'src/app/Models/student';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class SchoolAdminService {

  baseUrl: string = environment.baseUrl;

  schoolNo: any = {
    schoolID: 0
  };

  sNc: any = {
    schoolID: 0,
    ClassArmID: 0
  };

  school: School = {
    SchoolID: 0,
    Name: "",
    Address: "",
    LgaID: 0,
    ShowPosition: false,
    Append: "",
    ProprietorID: "",//fix
    Type: SchoolType.None,
    SsCompulsories: [],
    SsDrops: [],
  };


  principal: Principal = {
    FirstName: "",
    LastName: "",
    Address: "",
    Email: "",
    SchoolID: 0,//fix
    LgaID: 0,
    Gender: Gender,
    PasswordHash: ""
  }



  constructor(private http: HttpClient) { }

  addPrincipal() {
    return this.http.post(this.baseUrl + '/api/principal/addPrincipal', this.principal).toPromise();
  }

  getClasses() {
    return this.http.post<any[]>(this.baseUrl + '/api/class/getClass', this.schoolNo).toPromise();
  }

  getClassArms() {
    return this.http.post<any[]>(this.baseUrl + '/api/class/getClassArms', this.schoolNo).toPromise();
  }

  addStudent(Student: Student) {
    Student.SchoolID = this.schoolNo.schoolID;
    return this.http.post<any[]>(this.baseUrl + '/api/principal/AddStudent', Student).toPromise();
  }

  addTeacher(teacher: Student) {
    teacher.SchoolID = this.schoolNo.schoolID;
    return this.http.post<any[]>(this.baseUrl + '/api/principal/AddTeacher', teacher).toPromise();
  }

  getStudent() {
    return this.http.post<any[]>(this.baseUrl + '/api/principal/getStudents', this.schoolNo).toPromise();
  }

  getTeachers() {
    return this.http.post<any[]>(this.baseUrl + '/api/principal/getTeachers', this.schoolNo).toPromise();
  }

  getClassSubjects() {
    return this.http.post<any[]>(this.baseUrl + '/api/subject/getClassSubjects', this.schoolNo).toPromise();
  }

  getSubjects() {
    return this.http.post<any[]>(this.baseUrl + '/api/subject/getSubjects', this.schoolNo).toPromise();
  }

  getSubjectClasses(subid: number) {
    var SC: any = {
      schoolID: 0,
      SubjectID: subid
    };
    SC.schoolID = this.schoolNo.schoolID;
    console.log(SC);
    return this.http.post<any[]>(this.baseUrl + '/api/subject/getSubjectClasses', SC).toPromise();
  }

  filterStudent(CAI: number) {
    var sNc: any = {
      schoolID: 0,
      ClassArmID: CAI
    };
    sNc.schoolID = this.schoolNo.schoolID;

    return this.http.post<any[]>(this.baseUrl + '/api/principal/filterStudents', sNc).toPromise();
  }

  findStudent(SQ: string) {
    var FS: any = {
      schoolID: 0,
      searchQuery: SQ
    };
    FS.schoolID = this.schoolNo.schoolID;

    return this.http.post<any[]>(this.baseUrl + '/api/principal/findStudents', FS).toPromise();
  }

  findTeachers(SQ: string) {
    var FT: any = {
      schoolID: 0,
      searchQuery: SQ
    };
    FT.schoolID = this.schoolNo.schoolID;

    return this.http.post<any[]>(this.baseUrl + '/api/principal/findTeachers', FT).toPromise();
  }

  addTeacherSubject(TS:any){
    return this.http.post<any[]>(this.baseUrl + '/api/principal/assingSubject', TS).toPromise();
  }

  dissociateSubject(id : number){
    return this.http.get<any[]>(this.baseUrl + '/api/principal/dissociateSubject/?id='+ id).toPromise();
  }

  deleteTeacher(Id: string){
    var FT: any = {
      schoolID: 0,
      Id: Id
    };
    FT.schoolID = this.schoolNo.schoolID;
    return this.http.post<any[]>(this.baseUrl + '/api/principal/deleteTeacher', FT).toPromise();
  }

  EditStudent(Student: Student) {
    return this.http.post<any[]>(this.baseUrl + '/api/principal/editStudent', Student).toPromise();
  }

  deleteStudent(Id: string){
    var FT: any = {
      schoolID: 0,
      Id: Id
    };
    FT.schoolID = this.schoolNo.schoolID;
    return this.http.post<any[]>(this.baseUrl + '/api/principal/deleteStudent', FT).toPromise();
  }
}
