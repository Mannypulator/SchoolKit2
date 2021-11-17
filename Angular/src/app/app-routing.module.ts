import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AdminroleGuard } from './resources/adminrole.guard';
import { SchoolAdminGuard } from './resources/school-admin.guard';
import { PageNotFoundComponent } from './shared/components/page-not-found/page-not-found.component';


const routes: Routes = [
  //{path: "school", canActivate: [AuthGuard],
  // children:[
  {
    path: 'admin',
    loadChildren:
      () => import('./admin/admin.module').then(m => m.AdminModule),
    canLoad: [AdminroleGuard]
  },
  {
    path: 'school-admin',
    loadChildren:
      () => import('./school-admin/school-admin.module').then(m => m.SchoolAdminModule),
    canLoad: [SchoolAdminGuard] 
  },

  {
    path: 'teacher',
    loadChildren:
      () => import('./teacher/teacher.module').then(m => m.TeacherModule)
  },
  {
    path: 'student',
    loadChildren:
      () => import('./student/student.module').then(m => m.StudentModule)
  },
  // ]
  // },
  { path: '', redirectTo: 'login', pathMatch: 'full' },
  {
    path: 'login',
    loadChildren:
      () => import('./login/login.module').then(m => m.LoginModule)
  },
  { path: '**', component: PageNotFoundComponent }

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
