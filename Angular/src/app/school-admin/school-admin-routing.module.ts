import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ProprietorGuard } from '../resources/Proprietor.guard';
import { ResultViewComponent } from '../shared/components/result-view/result-view.component';
import { AddPrincipalComponent } from './add-principal/add-principal.component';
import { AddSchoolsComponent } from './add-school/add-schools.component';
import { AddStudentComponent } from './add-student/add-student.component';
import { AddTeacherComponent } from './add-teacher/add-teacher.component';
import { PrincipalDashboardComponent } from './principal-dashboard/principal-dashboard.component';

import { SchoolAdminComponent } from './school-admin/school-admin.component';
import { SchoolSelectionComponent } from './school-selection/school-selection.component';
import { SessionComponent } from './session/session.component';
import { SettingsComponent } from './settings/settings.component';
import { StudentResultsComponent } from './student-results/student-results.component';
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
        path: 'settings', component: SettingsComponent,
      },
      {
        path: 'sessions', component: SessionComponent,
      },
      {
        path: 'student-results', component: StudentResultsComponent,
      },
      {
        path: 'result-view/:id', component: ResultViewComponent,
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
