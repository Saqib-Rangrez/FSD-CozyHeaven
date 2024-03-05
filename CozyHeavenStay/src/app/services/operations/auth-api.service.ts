import { HttpClient } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { BehaviorSubject, Observable, throwError } from 'rxjs';
import { catchError, finalize, tap } from 'rxjs/operators';
import { endpoints } from '../apis';
import { ToastrService } from 'ngx-toastr';
import { RegisterUserDTO } from '../../models/DTO/RegisterUserDTO';
import { RegisterAdminDTO } from '../../models/DTO/RegisterAdminDTO';
import { User } from '../../models/User.Model';

@Injectable({
  providedIn: 'root'
})

export class AuthAPIService {

  http : HttpClient = inject(HttpClient);
  user = new BehaviorSubject<User>(null);
  toastr : ToastrService = inject(ToastrService);

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



  private handleCreateUser(res){
    const expiresInTs = new Date().getTime() + +res.expiresIn * 1000;
    const expiresIn = new Date(expiresInTs);
    const user = new User();
    this.user.next(user);
    this.autoLogout(res.expiresIn * 1000);

    localStorage.setItem('user', JSON.stringify(user));
}

}
