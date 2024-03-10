import { Injectable, inject } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate  {

  constructor(private router:Router, private jwtHelper: JwtHelperService){}
  
  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
    const user = JSON.parse(localStorage.getItem("user"));

    if (user?.token && !this.jwtHelper.isTokenExpired(user?.token)){
      console.log(this.jwtHelper.decodeToken(user?.token));
      return true;
    }

    this.router.navigate(["/login"]);
    return false;
  }
}

export const OpenRoute = () => {
    const user = JSON.parse(localStorage.getItem("user"));
    const router = inject(Router);

    if (!user?.token){
        return true;
    }
    router.navigate(["/dashboard/profile"]);
    return false;  
}