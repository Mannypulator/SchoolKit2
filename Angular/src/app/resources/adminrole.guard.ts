import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree, Router, CanLoad, Route, UrlSegment } from '@angular/router';
import { Observable } from 'rxjs';
import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root'
})
export class AdminroleGuard implements CanLoad {
  /**
   *
   */
  constructor(private router: Router, private authService: AuthService) {
  }
  canLoad(route: Route): boolean | UrlTree {
    
    if(this.authService.isAdmin()){
      console.log(this.authService.currentUser);
      return true}
      
      return this.router.parseUrl('login');
  }
  
  
}
