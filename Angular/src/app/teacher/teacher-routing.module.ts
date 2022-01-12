import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { TeacherAuth } from '../resources/teacher.guard';
import { ProfileComponent } from './profile/profile.component';
import { SubjectComponent } from './subject/subject.component';
import { TeacherComponent } from './teacher/teacher.component';

const routes: Routes = [
  { path: '', component: TeacherComponent, canActivate: [TeacherAuth],
  children: [
    {path: 'profile', component: ProfileComponent},
    {path: 'subject/:id/:title/:arm', component: SubjectComponent},
    {path: '', redirectTo: 'profile'}

  ]
}
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class TeacherRoutingModule { }
