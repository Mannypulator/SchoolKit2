import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ProprietorGuard } from '../resources/Proprietor.guard';
import { AddPrincipalComponent } from './add-principal/add-principal.component';
import { AddSchoolsComponent } from './add-school/add-schools.component';
import { AddStudentComponent } from './add-student/add-student.component';
import { AddTeacherComponent } from './add-teacher/add-teacher.component';
import { PrincipalDashboardComponent } from './principal-dashboard/principal-dashboard.component';
import { SchoolAdminComponent } from './school-admin/school-admin.component';
import { SchoolSelectionComponent } from './school-selection/school-selection.component';
import { StudentsComponent } from './students/students.component';
import { TeachersComponent } from './teachers/teachers.component';

const routes: Routes = [
  {
    path: '', component: SchoolAdminComponent, canActivate: [ProprietorGuard],
    children: [
      {
        path: 'principal-dashboard', component: PrincipalDashboardComponent

      },
      {
        path: 'students', component: StudentsComponent,
      },
      {
        path: 'teachers', component: TeachersComponent,
      },
      {
        path: 'add-school', component: AddSchoolsComponent,
      },
      {
        path: 'add-principal', component: AddPrincipalComponent,
      },
      {
        path: '', redirectTo: 'principal-dashboard', pathMatch: 'full'

      },

    ]
  },
  {
    path: 'school-selection', component: SchoolSelectionComponent,
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class SchoolAdminRoutingModule { }
