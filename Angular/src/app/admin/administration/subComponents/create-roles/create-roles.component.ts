import { Component, OnInit } from '@angular/core';
import { AlertService } from 'ngx-alerts';
import { AdminService } from 'src/app/resources/admin.service';

@Component({
  selector: 'app-create-roles',
  templateUrl: './create-roles.component.html',
  styleUrls: ['./create-roles.component.css']
})
export class CreateRolesComponent implements OnInit {

  constructor(public admin: AdminService, private alert: AlertService) { }

  role: string = "";
  ngOnInit(): void {
  }

  submitRole(){
    this.alert.info('Please wait'); 
    this.admin.addRole(this.role).subscribe((res:any)=>{
      this.alert.success('Role succesfully added');
    },
    err=> {this.alert.danger('Failed to add role');
  console.log(err)});
  }

}
