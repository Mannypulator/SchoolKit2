import { Injectable } from '@angular/core';
import { RouterStateSnapshot, UrlTree, Router, CanLoad, Route, UrlSegment, CanActivate, ActivatedRouteSnapshot } from '@angular/router';
import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root'
})
export class StudentRoleGuard implements CanLoad {
  /**
   *
   */
   state!: RouterStateSnapshot
  constructor(private router: Router, private authService: AuthService) {
  }
  canLoad(route: Route, segments: UrlSegment[]): true|UrlTree { 
    //const url = segments.map(s => s.path).join('/');
   
      
    return this.checkLogin();
  }

  checkLogin(): true|UrlTree {
    if(this.authService.isStudent()){
      return true}
    // Redirect to the login page 
    return this.router.parseUrl('login');
  }
  
  
}


@Injectable({
  providedIn: 'root'
})
export class StudentAuth implements CanActivate {
  constructor(private authService: AuthService, private router: Router) {}

  canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): true|UrlTree {
    const url: string = state.url;
    this.authService.redirectUrl = url;
    if(this.authService.isStudent()){
      return true}
    // Redirect to the login page 
    return this.router.parseUrl('login');
    }
  }
