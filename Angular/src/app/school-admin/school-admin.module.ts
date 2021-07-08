import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { SchoolAdminRoutingModule } from './school-admin-routing.module';
import { PrincipalDashboardComponent } from './principal-dashboard/principal-dashboard.component';
import { AddTeacherComponent } from './add-teacher/add-teacher.component';
import { AddStudentComponent } from './add-student/add-student.component';
import { RemoveTeacherComponent } from './remove-teacher/remove-teacher.component';
import { RemoveStudentComponent } from './remove-student/remove-student.component';
import { SchoolAdminComponent } from './school-admin/school-admin.component';
import { SharedModuleModule } from '../shared/shared-module/shared-module.module';
import { SchoolsComponent } from './schools/schools.component';
import { PrincipalSidenavListComponent } from './principal-sidenav-list/principal-sidenav-list.component';
import { TeachersComponent } from './teachers/teachers.component';
import { StudentsComponent } from './students/students.component';
import { SchoolSelectionComponent } from './school-selection/school-selection.component';


@NgModule({
  declarations: [PrincipalDashboardComponent, AddTeacherComponent, AddStudentComponent, RemoveTeacherComponent, RemoveStudentComponent, SchoolAdminComponent, SchoolsComponent, PrincipalSidenavListComponent, TeachersComponent, StudentsComponent, SchoolSelectionComponent],
  imports: [
    CommonModule,
    SchoolAdminRoutingModule,
    SharedModuleModule
  ]
})
export class SchoolAdminModule { }
