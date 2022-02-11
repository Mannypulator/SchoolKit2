import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { SchoolAdminRoutingModule } from './school-admin-routing.module';
import { PrincipalDashboardComponent } from './principal-dashboard/principal-dashboard.component';
import { AddTeacherComponent } from './add-teacher/add-teacher.component';
import { AddStudentComponent } from './add-student/add-student.component';
import { SchoolAdminComponent } from './school-admin/school-admin.component';
import { SharedModuleModule } from '../shared/shared-module/shared-module.module';
import { AddSchoolsComponent } from './add-school/add-schools.component';
import { PrincipalSidenavListComponent } from './principal-sidenav-list/principal-sidenav-list.component';
import { TeachersComponent } from './teachers/teachers.component';
import { StudentsComponent } from './students/students.component';
import { SchoolSelectionComponent } from './school-selection/school-selection.component';
import { SchoolAdminService } from './Services/school-admin.service';
import { SchoolsComponent } from './schools/schools.component';
import { SchoolAdministrationComponent } from './school-administration/school-administration.component';
import { AddPrincipalComponent } from './add-principal/add-principal.component';
import { AssignSubjectComponent } from './assign-subject/assign-subject.component';
import { EditStudentComponent } from './edit-student/edit-student.component';
import { SettingsComponent } from './settings/settings.component';
import { SessionComponent } from './session/session.component';
import { AddSessionsComponent } from './add-sessions/add-sessions.component';
import { StudentResultsComponent } from './student-results/student-results.component';
import { ClassSelectionComponent } from './class-selection/class-selection.component';

import { AssignClassComponent } from './assign-class/assign-class.component';


@NgModule({
  declarations: [PrincipalDashboardComponent, AddTeacherComponent, AddStudentComponent, SchoolAdminComponent, AddSchoolsComponent, PrincipalSidenavListComponent, TeachersComponent, StudentsComponent, SchoolSelectionComponent, SchoolsComponent, SchoolAdministrationComponent, AddPrincipalComponent, AssignSubjectComponent, EditStudentComponent, SettingsComponent, SessionComponent, AddSessionsComponent, StudentResultsComponent, ClassSelectionComponent,  AssignClassComponent], 
  imports: [
    CommonModule,
    SchoolAdminRoutingModule,
    SharedModuleModule
  ],
  providers: [
    SchoolAdminService,
  ],
})
export class SchoolAdminModule { }
 