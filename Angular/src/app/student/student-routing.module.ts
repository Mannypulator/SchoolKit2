import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { PinValidationComponent } from './pin-validation/pin-validation.component';
import { ResultComponent } from './result/result.component';

const routes: Routes = [
  {path:'pin-validation', component: PinValidationComponent},
  {path: 'result', component: ResultComponent}
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class StudentRoutingModule { }
