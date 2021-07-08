import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AddStudentComponent } from './add-student/add-student.component';
import { AddTeacherComponent } from './add-teacher/add-teacher.component';
import { PrincipalDashboardComponent } from './principal-dashboard/principal-dashboard.component';
import { SchoolAdminComponent } from './school-admin/school-admin.component';
import { StudentsComponent } from './students/students.component';
import { TeachersComponent } from './teachers/teachers.component';

const routes: Routes = [
  {
    path: '', component: SchoolAdminComponent,
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
        path: '', redirectTo: 'principal-dashboard', pathMatch: 'full'

      },

    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class SchoolAdminRoutingModule { }
