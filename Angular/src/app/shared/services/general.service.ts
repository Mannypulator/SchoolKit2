import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';


@Injectable({
  providedIn: 'root'
})
export class GeneralService {

  baseUrl: string = environment.baseUrl;
  constructor(private http: HttpClient) { }

  getStudentResult(Id: string){
    
   
    return this.http.get<any>(this.baseUrl + '/api/result/getStudentResult/?Id=' + Id).pipe(map(data=>{
      data.forEach((element: { PrincipalComment: string | null; }) => {
        if(element.PrincipalComment === null){
         element.PrincipalComment = "";
        }
      });
      return data;
    }))
  }

  submitComment(obj:  any){
    return this.http.post<any>(this.baseUrl + '/api/result/updatePComment', obj).toPromise()
  }

  submitTComment(obj:  any){
    return this.http.post<any>(this.baseUrl + '/api/result/updateTComment', obj).toPromise()
  }

  gradeA(obj : any){
    console.log(obj);
    return this.http.post<any>(this.baseUrl + '/api/result/gradeADomain', obj).toPromise()
  }
  gradeP(obj : any){
    return this.http.post<any>(this.baseUrl + '/api/result/gradePDomain', obj).toPromise()
  }

}
