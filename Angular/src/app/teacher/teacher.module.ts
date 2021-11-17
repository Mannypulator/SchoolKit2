import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { TeacherRoutingModule } from './teacher-routing.module';
import { ProfileComponent } from './profile/profile.component';
import { TeacherComponent } from './teacher/teacher.component';
import { SubjectComponent } from './subject/subject.component';
import { SharedModuleModule } from '../shared/shared-module/shared-module.module';
import { TeacherSidenavComponent } from './teacher-sidenav/teacher-sidenav.component';
import { FilterPipe } from '../resources/filter.pipe';


@NgModule({
  declarations: [ProfileComponent, TeacherComponent, SubjectComponent, TeacherSidenavComponent, FilterPipe,],
  imports: [
    CommonModule,
    TeacherRoutingModule,
    SharedModuleModule
  ]
})
export class TeacherModule { }
