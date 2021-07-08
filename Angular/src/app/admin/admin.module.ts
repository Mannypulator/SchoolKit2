import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';



import { AdminRoutingModule } from './admin-routing.module';
import { AdminComponent } from './admin.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { SharedModuleModule } from '../shared/shared-module/shared-module.module';
import { AdministrationComponent } from './administration/administration.component';
import { CreateSubjectsComponent } from './administration/subComponents/create-subjects/create-subjects.component';
import { CreateRolesComponent } from './administration/subComponents/create-roles/create-roles.component';
import { RegisterSchoolsComponent } from './administration/subComponents/register-schools/register-schools.component';
import { CreateAdminComponent } from './administration/subComponents/create-admin/create-admin.component';
import { DeleteAdminComponent } from './administration/subComponents/delete-admin/delete-admin.component';
import { RevokeAdminAccessComponent } from './administration/subComponents/revoke-admin-access/revoke-admin-access.component';
import { SidenavListComponent } from './sidenav-list/sidenav-list.component';
import { AdministrationSidenavComponent } from './administration/administration-sidenav/administration-sidenav.component';






@NgModule({
  declarations: [AdminComponent, DashboardComponent, AdministrationComponent, 
    CreateSubjectsComponent, CreateRolesComponent, RegisterSchoolsComponent, 
    CreateAdminComponent, DeleteAdminComponent, RevokeAdminAccessComponent, SidenavListComponent, AdministrationSidenavComponent],  
  imports: [ 
    CommonModule,
    AdminRoutingModule,
    SharedModuleModule
   
  ]
})
export class AdminModule { }
