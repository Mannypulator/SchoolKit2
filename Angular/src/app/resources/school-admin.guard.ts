import { Injectable } from '@angular/core';
import { CanLoad, Route, UrlSegment, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root'
})
export class SchoolAdminGuard implements CanLoad {

  /**
   *
   */
  constructor(private authService: AuthService, private router: Router) {
    
  }
  canLoad(
    route: Route,
    segments: UrlSegment[]): true | UrlTree {
      const url = segments.map(s => s.path).join('/');
      if(url != 'school-admin/school-selection'){
         this.authService.redirectUrl = url;
       }
      return this.checkLogin(url);
  }

  checkLogin(url: string):true| UrlTree{
    if(this.authService.isPrincipal() || this.authService.isProprietor()){
      
      return true}

    // Redirect to the login page 
    return this.router.parseUrl('login');
  }
}
