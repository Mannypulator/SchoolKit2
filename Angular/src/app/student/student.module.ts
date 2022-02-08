import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { StudentRoutingModule } from './student-routing.module';
import { StudentComponent } from './student/student.component';
import { PinValidationComponent } from './pin-validation/pin-validation.component';
import { ResultComponent } from './result/result.component';
import { SharedModuleModule } from '../shared/shared-module/shared-module.module';
import { StudentSidenavComponent } from './student-sidenav/student-sidenav.component';
import { ProfileComponent } from './profile/profile.component';
import { ResultSelectionComponent } from './result-selection/result-selection.component';
import { EnrollmentsComponent } from './enrollments/enrollments.component';
import { EnrollmentListComponent } from './enrollment-list/enrollment-list.component';
import { ResultViewSComponent } from './result-view-s/result-view-s.component';


@NgModule({
  declarations: [StudentComponent, PinValidationComponent, ResultComponent, StudentSidenavComponent, ProfileComponent, ResultSelectionComponent, EnrollmentsComponent, EnrollmentListComponent, ResultViewSComponent],
  imports: [
    CommonModule,
    StudentRoutingModule,
    SharedModuleModule
  ]
})
export class StudentModule { }
