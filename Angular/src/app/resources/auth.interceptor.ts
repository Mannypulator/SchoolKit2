import { HttpHandler, HttpInterceptor, HttpRequest, HttpEvent } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Router } from "@angular/router";
import { Observable } from "rxjs";
import { tap } from "rxjs/operators";
import { AuthService } from "./auth.service";

@Injectable()
export class AuthInterceptor implements HttpInterceptor {

  constructor(private auth: AuthService, private router: Router) {}

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    if(this.auth.isLoggedIn()){
        const authReq = req.clone({
            headers: req.headers.set('Authorization', 'Bearer ' + localStorage.getItem('token'))
          });

          return next.handle(authReq).pipe(
              tap(
                  succ=>{},
                  err=>{
                      if(err.status == 401){
                          this.router.navigateByUrl('login')
                      }
                  }
              )
          );
    }
    else{
        return next.handle(req.clone());
    }
    

    // send cloned request with header to the next handler.
    
  }
}