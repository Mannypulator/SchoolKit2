import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { TeacherRoutingModule } from './teacher-routing.module';
import { ProfileComponent } from './profile/profile.component';
import { TeacherComponent } from './teacher/teacher.component';
import { SubjectComponent } from './subject/subject.component';
import { SharedModuleModule } from '../shared/shared-module/shared-module.module';
import { TeacherSidenavComponent } from './teacher-sidenav/teacher-sidenav.component';
import { FilterPipe } from '../resources/filter.pipe';
import { EnrollStudentComponent } from './enroll-student/enroll-student.component';
import { StudentResultsComponent } from './student-results/student-results.component';

import { ResultViewTComponent } from './result-view-t/result-view-t.component';



@NgModule({
  declarations: [ProfileComponent, TeacherComponent, SubjectComponent, TeacherSidenavComponent, FilterPipe, EnrollStudentComponent, StudentResultsComponent, ResultViewTComponent],
  imports: [
    CommonModule,
    TeacherRoutingModule,
    SharedModuleModule
  ]
})
export class TeacherModule { }
