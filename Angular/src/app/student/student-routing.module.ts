import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { StudentAuth } from '../resources/student.guard';
import { EnrollmentListComponent } from './enrollment-list/enrollment-list.component';
import { EnrollmentsComponent } from './enrollments/enrollments.component';
import { PinValidationComponent } from './pin-validation/pin-validation.component';
import { ProfileComponent } from './profile/profile.component';
import { ResultSelectionComponent } from './result-selection/result-selection.component';

import { ResultComponent } from './result/result.component';
import { StudentComponent } from './student/student.component';

const routes: Routes = [
  {path:'',component:StudentComponent,
  canActivate:[StudentAuth],
  children:[
    {path:'pin-validation', component: PinValidationComponent},
  {path: 'result-selection', component: ResultSelectionComponent},
  {path: 'enrollments', component: EnrollmentsComponent},
  {path: 'enrollment-list', component: EnrollmentListComponent},
  {path:'profile', component: ProfileComponent},
  {path:'profile', component: ProfileComponent},
  {path: '', component: ProfileComponent},
  ],
},
{path: 'result', component: ResultComponent},
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class StudentRoutingModule { }
