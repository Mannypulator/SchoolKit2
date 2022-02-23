import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class StudentService {

  baseUrl: string = environment.baseUrl;

  constructor(private http: HttpClient) { }

  getResultList(){
    return this.http.get<any[]>(this.baseUrl + '/api/result/resultList').toPromise();
  }

  getStudentResult(ResultID: string){
    return this.http.get<any>(this.baseUrl + '/api/result/getReportCard/?ResultID=' + ResultID,)
  }
}
