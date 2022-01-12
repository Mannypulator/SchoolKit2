import { HttpClient } from '@angular/common/http';
import { Inject } from '@angular/core';
import {AfterViewInit, Component, ViewChild, OnInit} from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import {MatPaginator} from '@angular/material/paginator';
import {MatSort} from '@angular/material/sort';
import {MatTableDataSource} from '@angular/material/table';
import { AlertService } from 'ngx-alerts';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-enroll-student',
  templateUrl: './enroll-student.component.html',
  styleUrls: ['./enroll-student.component.css'],
  
  
})
export class EnrollStudentComponent implements OnInit, AfterViewInit {
  baseUrl: string = environment.baseUrl;
  displayedColumns: string[] = ['FirstName', 'LastName', " "];
  dataSource: MatTableDataSource<any> = new MatTableDataSource();
 
  constructor(public dialogRef: MatDialogRef<EnrollStudentComponent>, @Inject(MAT_DIALOG_DATA) public data: any, private http: HttpClient, private alert: AlertService) {}
  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;

  }

  ngOnInit(): void {
   this.getUnEnrolled();
  }

  getUnEnrolled(){
    this.http.get<any[]>(this.baseUrl + '/api/enrollment/getUnEnrolled/?id=' + this.data).subscribe(
      res=>{
        this.dataSource = new MatTableDataSource(res);
        this.dataSource.paginator = this.paginator;
      },
      err=> console.log(err)
    );
  }

addStudent(f:any){
  this.alert.info('please wait')
  let enrollment: any = {
    StudentID : f,
    ClassSubjectID : this.data
  };
  this.http.post(this.baseUrl + '/api/teacher/enrollStudent', enrollment ).subscribe(
    res=>{
     this.alert.success('student added successfully');
     this.getUnEnrolled();
    },
    err=> {console.log(err);
    this.alert.danger('operation failed')}
  );
}

applyFilter(event: Event) {
  const filterValue = (event.target as HTMLInputElement).value;
  this.dataSource.filter = filterValue.trim().toLowerCase();
}
 
  
}

