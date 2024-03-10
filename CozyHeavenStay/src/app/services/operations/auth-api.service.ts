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
import { Admin } from '../../models/Admin.Model';
import { ResetPasswordDTO } from '../../models/DTO/ResetPasswordDTO';
import { HotelOwner } from '../../models/hotel-owner.Model';

@Injectable({
  providedIn: 'root'
})

export class AuthAPIService {

  http: HttpClient = inject(HttpClient);
  user = new BehaviorSubject<User>(null);
  admin = new BehaviorSubject<Admin>(null);
  owner = new BehaviorSubject<User>(null);
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


  loginUser(email:string, password:string){

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

loginOwner(email:string, password:string){

  const loadingToast = this.toastr.info("Logging in", "Please wait...", {
    disableTimeOut: true,
    closeButton: false,
    positionClass: 'toast-top-center'
  });

  const data = {email: email, password: password};

  return this.http.post(
  endpoints.LOGIN_OWNER_API, data
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

loginAdmin(email:string, password:string){

  const loadingToast = this.toastr.info("Logging in", "Please wait...", {
    disableTimeOut: true,
    closeButton: false,
    positionClass: 'toast-top-center'
  });

  const data = {email: email, password: password};

  return this.http.post(
  endpoints.LOGIN_ADMIN_API, data
  ).pipe(catchError((error) => {
    if (loadingToast) {
      this.toastr.clear();
    }
    this.toastr.error('Failed to Login', 'Error');
      
    return throwError(() => error);
  }), tap((res) => {
      console.log(res);
      this.handleCreateUser(res)
  }),finalize(() => {
    if (loadingToast) {
      this.toastr.clear();
    }
  }));
}

  autoLogin () {
    const res = JSON.parse(localStorage.getItem('user'));

    if(res === null || res === undefined){
        return;
    }
    let user : any;

    if(res?.role == "Admin") {

      user = new Admin(
        res.adminId,
        res.firstName,
        res.lastName,
        res.email,
        res.password,
        res.profileImage,
        res.role,
        res.token,
        res.resetPasswordExpires,
        //res.expiresIn
      )
    }else if(res?.role == "Owner"){
      user = new User(
        res.userId,
        res.firstName,
        res.lastName,
        res.email,
        res.password,
        res.gender,
        res.contactNumber,
        res.address,
        //res.expiresIn,
        res.role,
        res.profileImage,
        res.token,
        res.resetPasswordExpires,
    );
    }else{
      user = new User(
        res.userId,
        res.firstName,
        res.lastName,
        res.email,
        res.password,
        res.gender,
        res.contactNumber,
        res.address,
        res.expiresIn,  
        res.role,
        res.profileImage,
        res.token,
        //res.resetPasswordExpires,
    );
    }  

    if(user.token){
        this.user.next(user);
        const timerValue =  (3*60*60*1000) //user?.expiresIn.getTime - new Date().getTime() ||;
        this.autoLogout(timerValue);
    }
  }

  logout (token : string) {

    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        'Authorization': `Bearer ${token}` 
      })
    };

    this.user.next(null);
    this.owner.next(null);
    this.admin.next(null);

    this.router.navigate(['/login']);
    localStorage.removeItem('user');

    if(this.tokenExpiretimer){
        clearTimeout(this.tokenExpiretimer);
    }
    this.tokenExpiretimer = null;

    return this.http.post(endpoints.LOGOUT_API, null, httpOptions).pipe(
      catchError(error => {

        this.toastr.error('Failed to logout', 'Error');
        
        return throwError(() => error);
      }))
  }

  autoLogout(expireTime: number){
    const res = JSON.parse(localStorage.getItem('user'));
    this.tokenExpiretimer = setTimeout(() => {
        this.logout(res?.token);
    }, (3*60*60*1000));
  }

  forgetPassword(email:string){

    const loadingToast = this.toastr.info("Sending Email", "Please wait...", {
      disableTimeOut: true,
      closeButton: false,
      positionClass: 'toast-top-center'
    });
  
    const data = {email: email};
  
    return this.http.post(
    endpoints.FORGETPASSWORD_API, data
    ).pipe(catchError((error) => {
      if (loadingToast) {
        this.toastr.clear();
      }
      this.toastr.error('Failed to sent email', 'Error');
        
      return throwError(() => error);
    }), tap((res) => {
        console.log(res);
    }),finalize(() => {
      if (loadingToast) {
        this.toastr.clear();
      }
    }));
  }

  resetPassword(data : ResetPasswordDTO){

    const loadingToast = this.toastr.info("Sending Email", "Please wait...", {
      disableTimeOut: true,
      closeButton: false,
      positionClass: 'toast-top-center'
    });
    
    return this.http.post(
    endpoints.RESETPASSWORD_API, data
    ).pipe(catchError((error) => {
      if (loadingToast) {
        this.toastr.clear();
      }
      this.toastr.error('Failed to reset password', 'Error');
        
      return throwError(() => error);
    }), tap((res) => {
        console.log(res);
    }),finalize(() => {
      if (loadingToast) {
        this.toastr.clear();
      }
    }));
  }

  private handleCreateUser(res){
    const expiresInTs = new Date().getTime() + ((3 * 60 * 60) * 1000);
    const expiresIn = new Date(expiresInTs);
    let user : any;
    console.log("from services handle...." ,res.user)

    if(res.user.role == "Admin") {
      user = new Admin(
        res.user.adminId,
        res.user.firstName,
        res.user.lastName,
        res.user.email,
        res.user.password,
        res.user.profileImage,
        res.user.role,
        res.user.token,
        res.user.resetPasswordExpires,
        //expiresIn
      )
      this.admin.next(user);

    }else if(res.user.role == "Owner"){
      user = new User(
        res.user.ownerId,
        res.user.firstName,
        res.user.lastName,
        res.user.email,
        res.user.password,
        res.user.gender,
        res.user.contactNumber,
        res.user.address,
        res.user.role,
        res.user.profileImage,
        res.user.token,
        res.user.resetPasswordExpires,
        //expiresIn
    );
    this.owner.next(user);

    }else{
      user = new User(
        res.user.userId,
        res.user.firstName,
        res.user.lastName,
        res.user.email,
        res.user.password,
        res.user.gender,
        res.user.contactNumber,
        res.user.address,
        //expiresIn,
        res.user.role,
        res.user.profileImage,
        res.user.token,
        res.user.resetPasswordExpires,
    );
    this.user.next(user);
    }
      
    
    this.autoLogout(1000);

    localStorage.setItem("user" , JSON.stringify(user));
  }
}
