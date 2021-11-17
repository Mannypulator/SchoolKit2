import { Component, OnInit, Pipe, PipeTransform } from '@angular/core';
import { AlertService } from 'ngx-alerts';
import { sRange } from 'src/app/Models/sRange';
import { Subject } from 'src/app/Models/Subject';
import { AdminService } from 'src/app/resources/admin.service';


@Component({
  selector: 'app-create-subjects',
  templateUrl: './create-subjects.component.html',
  styleUrls: ['./create-subjects.component.css'] 
})

export class CreateSubjectsComponent implements OnInit {

  constructor(public admin: AdminService, private alert: AlertService) { }
  subject : Subject = {
    SubjectID: 0,
    Title : "",
    Range : sRange.All,
    SchoolSpecific: false,
    
  }

  subjectRange! : string[];
  ngOnInit(): void {
    this.subjectRange = [];
    for (let subject in sRange) {
      if (isNaN(Number(subject))) {
          this.subjectRange.push(subject);
      }
  }
  }
  

  onSubmit(){
    console.log(this.subject);
    this.alert.info('Please wait'); 
    this.admin.addSubject(this.subject).subscribe((res)=>{
      this.alert.success('Subject added succesfully');
    },
    err => {
      this.alert.danger('Failed to add subject');
      console.log(err);
    }

    )
  }

}
