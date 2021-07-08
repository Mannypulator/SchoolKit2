import { Component, OnInit } from '@angular/core';
import { AlertService } from 'ngx-alerts';
import { AdminService } from 'src/app/resources/admin.service';

@Component({
  selector: 'app-create-subjects',
  templateUrl: './create-subjects.component.html',
  styleUrls: ['./create-subjects.component.css']
})
export class CreateSubjectsComponent implements OnInit {

  constructor(public admin: AdminService, private alert: AlertService) { }
  subjectModel : any = {
    Title : "",
    Range : "",
    SchoolSpecific: false,
  }
  ngOnInit(): void {
    
  }

  onSubmit(){
    this.alert.info('Please wait'); 
    this.admin.addSubject(this.subjectModel).subscribe((res)=>{
      this.alert.success('Subject added succesfully');
    },
    err => {
      this.alert.danger('Failed to add subject');
      console.log(err);
    }

    )
  }

}
