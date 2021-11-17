import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root'
})
export class ProprietorGuard implements CanActivate {
  /**
   *
   */
  constructor(private authService: AuthService, private router: Router) {
   
    
  }
  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): true | UrlTree {
      if(this.authService.isProprietor()){
        return this.checkSchool();
      }
      
    return true;
  }

  checkSchool(): true | UrlTree{
    if(localStorage.getItem('selectedSchool') === null){
      return this.router.parseUrl('school-admin/school-selection')
    }
    else{
      return true;
    }
  }
  
}
