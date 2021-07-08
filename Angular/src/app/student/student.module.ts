import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { StudentRoutingModule } from './student-routing.module';
import { StudentComponent } from './student/student.component';
import { PinValidationComponent } from './pin-validation/pin-validation.component';
import { ResultComponent } from './result/result.component';
import { SharedModuleModule } from '../shared/shared-module/shared-module.module';


@NgModule({
  declarations: [StudentComponent, PinValidationComponent, ResultComponent],
  imports: [
    CommonModule,
    StudentRoutingModule,
    SharedModuleModule
  ]
})
export class StudentModule { }
