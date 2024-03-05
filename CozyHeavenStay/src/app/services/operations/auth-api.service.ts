import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { BehaviorSubject, Observable, throwError } from 'rxjs';
import { catchError, finalize, tap } from 'rxjs/operators';
import { endpoints } from '../apis';
import { ToastrService } from 'ngx-toastr';
import { RegisterUserDTO } from '../../models/DTO/RegisterUserDTO';
import { RegisterAdminDTO } from '../../models/DTO/RegisterAdminDTO';
import { User } from '../../models/User.Model';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})

export class AuthAPIService {

  http: HttpClient = inject(HttpClient);
  user = new BehaviorSubject<User>(null);
  toastr : ToastrService = inject(ToastrService);
  router : Router = inject(Router);
  private tokenExpiretimer : any;

  signupUser (user : RegisterUserDTO) {
    const loadingToast = this.toastr.info('Signing up...', 'Please wait', {
      disableTimeOut: true,
      closeButton: false,
      positionClass: 'toast-top-center'
    });

    return this.http.post(endpoints.SIGNUP_USER_API, user).pipe(
      catchError(error => {
        if (loadingToast) {
          this.toastr.clear();
        }
        this.toastr.error('Failed to sign up', 'Error');
        
        return throwError(() => error);
      }),
      finalize(() => {
        if (loadingToast) {
          this.toastr.clear();
        }
      })
    );
  }


  signupOwner (user : RegisterUserDTO) {
    const loadingToast = this.toastr.info('Signing up...', 'Please wait', {
      disableTimeOut: true,
      closeButton: false,
      positionClass: 'toast-top-center'
    });

    return this.http.post(endpoints.SIGNUP_OWNER_API, user).pipe(
      catchError(error => {
        if (loadingToast) {
          this.toastr.clear();
        }
        this.toastr.error('Failed to sign up', 'Error');
        
        return throwError(() => error);
      }),
      finalize(() => {
        if (loadingToast) {
          this.toastr.clear();
        }
      })
    );
  }

  signupAdmin(user : RegisterAdminDTO) {
    const loadingToast = this.toastr.info("Signing up admin", "Please wait...", {
      disableTimeOut: true,
      closeButton: false,
      positionClass: 'toast-top-center'
    });

    return this.http.post(endpoints.SIGNUP_ADMIN_API, user).pipe(
      catchError(error => {
        if (loadingToast) {
          this.toastr.clear();
        }
        this.toastr.error('Failed to sign up', 'Error');
        
        return throwError(() => error);
      }),
      finalize(() => {
        if (loadingToast) {
          this.toastr.clear();
        }
      })
    );
  }


  login(email:string, password:string){

    const loadingToast = this.toastr.info("Logging in", "Please wait...", {
      disableTimeOut: true,
      closeButton: false,
      positionClass: 'toast-top-center'
    });

    const data = {email: email, password: password};

    return this.http.post(
    endpoints.LOGIN_USER_API, data
    ).pipe(catchError((error) => {
      if (loadingToast) {
        this.toastr.clear();
      }
      this.toastr.error('Failed to Login', 'Error');
        
      return throwError(() => error);
    }), tap((res) => {
        this.handleCreateUser(res)
    }),finalize(() => {
      if (loadingToast) {
        this.toastr.clear();
      }
    }));
}

  autoLogin () {
    const res = JSON.parse(localStorage.getItem('user'));

    if(!res){
        return;
    }

    const user = new User();
    user.userId = res.userId;
    user.firstName = res.firstName;
    user.lastName = res.lastName;
    user.email = res.email;
    user.address = res.address;
    user.contactNumber = res.contactNumber;
    user.password = res.password;
    user.gender = res.gender;
    user.profileImage = res.profileImage;
    user.role = res.role;
    user.token = res.token;
    user.expiresIn = res.expiresIn;
    user.resetPasswordExpires = res.resetPasswordExpires;


    if(user.token){
        this.user.next(user);
        const timerValue = user.expiresIn.getTime() - new Date().getTime();
        this.autoLogout(timerValue);
    }
  }

  logout (token : string) {

    const loadingToast = this.toastr.info("Logging out", "Please wait...", {
      disableTimeOut: true,
      closeButton: false,
      positionClass: 'toast-top-center'
    });

    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        'Authorization': `Bearer ${token}` 
      })
    };


    this.http.post(endpoints.LOGOUT_API, null, httpOptions).pipe(
      catchError(error => {
        if (loadingToast) {
          this.toastr.clear();
        }
        this.toastr.error('Failed to logout', 'Error');
        
        return throwError(() => error);
      }), finalize(() => {
        if(loadingToast){
          this.toastr.clear();
        }
      })
    )

    this.user.next(null);
    this.router.navigate(['/login']);
    localStorage.removeItem('user');

    if(this.tokenExpiretimer){
        clearTimeout(this.tokenExpiretimer);
    }
    this.tokenExpiretimer = null;
  }

  autoLogout(expireTime: number){
    const res = JSON.parse(localStorage.getItem('user'));
    this.tokenExpiretimer = setTimeout(() => {
        this.logout(res.token);
    }, expireTime);
  }


  private handleCreateUser(res){
    const expiresInTs = new Date().getTime() + (3 * 60 * 60 * 1000);
    const expiresIn = new Date(expiresInTs);

    const user = new User();
    user.userId = res.userId;
    user.firstName = res.firstName;
    user.lastName = res.lastName;
    user.email = res.email;
    user.address = res.address;
    user.contactNumber = res.contactNumber;
    user.password = res.password;
    user.gender = res.gender;
    user.profileImage = res.profileImage;
    user.role = res.role;
    user.token = res.token;
    user.expiresIn = expiresIn;
    user.resetPasswordExpires = res.resetPasswordExpires;
    
    this.user.next(user);
    this.autoLogout(res.expiresIn * 1000);

    localStorage.setItem('user', JSON.stringify(user));
  }

}
