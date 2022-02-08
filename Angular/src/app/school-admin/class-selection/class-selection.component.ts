import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { ClassArm } from 'src/app/Models/ClassArm';
import { AuthService } from 'src/app/resources/auth.service';
import { SchoolAdminService } from '../Services/school-admin.service';

@Component({
  selector: 'class-selection',
  templateUrl: './class-selection.component.html',
  styleUrls: ['./class-selection.component.css']
})
export class ClassSelectionComponent implements OnInit {
  @Output() public filter = new EventEmitter();
  constructor(private auth: AuthService, private admin: SchoolAdminService) { }
  classes: ClassArm[] = [];
  current!: number;
  ngOnInit(): void {
    this. getClasses();
  }
  onFilter(classArmID: number){
    this.current = classArmID;// store the current classID and use it to show which class is currently active
    this.filter.emit({classArmID});
  }
  getClasses() {
    this.admin.assignSchoolID()
    this.admin.getClasses().then((res)=>{
      this.classes = res as unknown as ClassArm[]
    },
      (err)=>{
        console.log(err)
      })
  }

}
