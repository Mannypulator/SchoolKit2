import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AdminComponent } from './admin.component';
import { AdministrationComponent } from './administration/administration.component';
import { CreateAdminComponent } from './administration/subComponents/create-admin/create-admin.component';
import { CreateRolesComponent } from './administration/subComponents/create-roles/create-roles.component';
import { CreateSubjectsComponent } from './administration/subComponents/create-subjects/create-subjects.component';
import { DeleteAdminComponent } from './administration/subComponents/delete-admin/delete-admin.component';
import { RegisterSchoolsComponent } from './administration/subComponents/register-schools/register-schools.component';
import { RevokeAdminAccessComponent } from './administration/subComponents/revoke-admin-access/revoke-admin-access.component';
import { DashboardComponent } from './dashboard/dashboard.component';



const routes: Routes = [
  {
    path:'', component: AdminComponent,
    children:[
      {
      path:'dashboard', component: DashboardComponent
      
    },
    {
      path:'administration', component: AdministrationComponent,
      children:[
        {
          path:'create-admin', component: CreateAdminComponent
          
        },
        {
          path:'create-roles', component: CreateRolesComponent
          
        },
        {
          path:'create-subjects', component: CreateSubjectsComponent
          
        },
        {
          path:'delete-admin', component: DeleteAdminComponent
        },
        {
          path:'register-schools', component: RegisterSchoolsComponent
        },
        {
          path:'revoke-admin-access', component: RevokeAdminAccessComponent
        },
        {
          path:'', redirectTo: 'register-schools', pathMatch:'full'
        }
      ]
      
    },
    {
      path:'', redirectTo: 'dashboard', pathMatch:'full'
    }
     
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AdminRoutingModule { }
