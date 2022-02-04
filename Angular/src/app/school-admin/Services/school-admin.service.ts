import { formatDate } from '@angular/common';
import { HttpClient, HttpEventType } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
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

  getRStudent() {
    return this.http.post<any[]>(this.baseUrl + '/api/term/getStudents', this.schoolNo).toPromise();
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

  filterRStudent(CAI: number) {
    var sNc: any = {
      schoolID: 0,
      ClassArmID: CAI
    };
    sNc.schoolID = this.schoolNo.schoolID;

    return this.http.post<any[]>(this.baseUrl + '/api/term/filterStudents', sNc).toPromise();
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

  addSession(sessionTitle : string) {
    var session: any = {
      SchoolID: 0,
      SessionName: sessionTitle
    };
    session.SchoolID = this.schoolNo.schoolID;
    console.log(session);
    return this.http.post<any[]>(this.baseUrl + '/api/term/createSession', session).toPromise();
  }

  getSessions() {
    return this.http.post<any[]>(this.baseUrl + '/api/term/getSessions', this.schoolNo).toPromise();
  }

  newTerm(term:any){
    return this.http.post<any[]>(this.baseUrl + '/api/term/startTerm', term).toPromise();
  }

  getCurrentSession(){
    var id: any = {
      SchoolID: 0,

    };
    id.SchoolID = this.schoolNo.schoolID;
    return this.http.post<any>(this.baseUrl + '/api/term/currentSession', id).pipe(
      map(data => {
        if(data != null && data.Terms.length != 0 ){
          console.log("it ran");
           data.Terms[0].TermStart = formatDate(data.Terms[0].TermStart, 'yyyy-MM-dd', 'en-US');
           
        }
        console.log(data);
        return data;
      })

    );
  }

  endTerm(Id: number){
    var id: any = {
      Id: Id
    };
    return this.http.post<any[]>(this.baseUrl + '/api/term/endTerm', id).toPromise();
  }

  compileResult(Id: number){
    var id: any = {
      Id: Id
    };
    return this.http.post<any[]>(this.baseUrl + '/api/term/compileResult', id).toPromise();
  }

  saveTestScheme(TestScheme: any){
    TestScheme.SchoolID = this.schoolNo.schoolID;
    return this.http.post<any>(this.baseUrl + '/api/principal/saveTestScheme', TestScheme).toPromise()
  }

  saveGradeScheme(GradeScheme: any){
    GradeScheme.SchoolID = this.schoolNo.schoolID;
    return this.http.post<any>(this.baseUrl + '/api/principal/saveGradeScheme', GradeScheme).toPromise()
  }

  getScoreScheme(){
    console.log(this.schoolNo.schoolID);
    return this.http.get<any>(this.baseUrl + '/api/principal/getScoreScheme/?SchoolID=' + this.schoolNo.schoolID).toPromise()
  }

  UploadFile(file:any){
    if(file.length == 0){
      return;
    }
    var id = (this.schoolNo.schoolID).toString();
    let fileToUpload = <File>file[0];
    const formData = new FormData();
    formData.append('file', fileToUpload,fileToUpload.name);
    formData.append('ID',id);

    return this.http.post<any>(this.baseUrl + '/api/principal/uploadlogo', formData, {reportProgress: true, observe: 'events'})
  }

  getStudentResult(Id : string){
    var id: any = {
      Id: Id,
      schoolID : 0
    };
    id.schoolID = this.schoolNo.schoolID;
    return this.http.post<any>(this.baseUrl + '/api/result/getStudentResult', id).toPromise()
  }

  submitComment(obj:  any){
    return this.http.post<any>(this.baseUrl + '/api/result/updateComment', obj).toPromise()
  }
}
