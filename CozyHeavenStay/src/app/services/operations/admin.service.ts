import { Injectable, inject } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { BehaviorSubject, Observable, throwError } from 'rxjs';
import { catchError, finalize, tap } from 'rxjs/operators';
import { User } from '../../models/User.Model';
import { adminEndpoints, userEndpoints } from '../apis';
import { ToastrService } from 'ngx-toastr';
import { Admin } from '../../models/Admin.Model';

@Injectable({
  providedIn: 'root'
})
export class AdminService {
    toastr : ToastrService = inject(ToastrService);
    http: HttpClient = inject(HttpClient);

    setToken(token: string ){
      const httpOptions = {
        headers: new HttpHeaders({
          'Content-Type': 'application/json',
          'Authorization': `Bearer ${token}` 
        })
      };
      return httpOptions;
    }

  getAllAdmin(token : string): Observable<Admin[]> { 
    return this.http.get<Admin[]>(adminEndpoints.GET_ALL_ADMINS_API, this.setToken(token))
      .pipe(
        catchError(this.handleError)
      );
  }

  getAdminById(id: number, token : string): Observable<any> { 
    return this.http.get<any>(`${adminEndpoints.GET_ADMIN_BY_ID_API}${id}`, this.setToken(token))
      .pipe(
        catchError(this.handleError)
      );
  }

  createUser(admin: Admin, token : string): Observable<Admin> { 
    return this.http.post<Admin>(adminEndpoints.CREATE_ADMIN_API, admin, this.setToken(token))
      .pipe(
        catchError(this.handleError)
      );
  }

  updateAdmin(admin: Admin, token : string): Observable<any> { 
    const loadingToast = this.toastr.info('Updating User...', 'Please wait', {
        disableTimeOut: true,
        closeButton: false,
        positionClass: 'toast-top-center'
      });

    return this.http.put<any>(`${adminEndpoints.UPDATE_ADMIN_API}`, admin, this.setToken(token))
      .pipe(
        tap((res) => {
            this.toastr.clear();
            console.log(res);
            this.handleCreateUser(res);
        }),
        catchError((err) => {
            if (loadingToast) {
                this.toastr.clear();
            }
            this.toastr.error('Failed to Update', 'Error');
            return throwError(() => err);
        }),
        finalize(() => {
            if (loadingToast) {
                this.toastr.clear();
            }
        })
      );
  }

  deleteUser(id: number, token : string): Observable<any> {
    const loadingToast = this.toastr.info('Updating User...', 'Please wait', {
      disableTimeOut: true,
      closeButton: false,
      positionClass: 'toast-top-center'
    });

    return this.http.delete<any>(`${adminEndpoints.DELETE_ADMIN_API}${id}`, this.setToken(token))
      .pipe(
        tap((res) => {
          this.toastr.clear();
          console.log(res);
        }),
        catchError((err) => {
          if (loadingToast) {
            this.toastr.clear();
          }
          this.toastr.error('Failed to Update', 'Error');
          return throwError(() => err);
        }),
        finalize(() => {
          if (loadingToast) {
            this.toastr.clear();
          }
        })        
      );
  }


  private handleError(error: any) {
    
    console.error('API Error:', error);
    return throwError('Something went wrong; please try again later.');
  }

  private handleCreateUser(res){
    const expiresInTs = new Date().getTime() + ((3 * 60 * 60) * 1000);
    const expiresIn = new Date(expiresInTs);
    let user : any;
    console.log("from services handle...." ,res)

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

     }
    //else if(res.user.role == "Owner"){
    //   user = new User(
    //     res.user.ownerId,
    //     res.user.firstName,
    //     res.user.lastName,
    //     res.user.email,
    //     res.user.password,
    //     res.user.gender,
    //     res.user.contactNumber,
    //     res.user.address,
    //     res.user.role,
    //     res.user.profileImage,
    //     res.user.token,
    //     res.user.resetPasswordExpires,
    //     //expiresIn
    // );

    // }else{
    //   user = new User(
    //     res.user.userId,
    //     res.user.firstName,
    //     res.user.lastName,
    //     res.user.email,
    //     res.user.password,
    //     res.user.gender,
    //     res.user.contactNumber,
    //     res.user.address,
    //     //expiresIn,
    //     res.user.role,
    //     res.user.profileImage,
    //     res.user.token,
    //     res.user.resetPasswordExpires,
    // );
    // }     
    
    localStorage.setItem("user" , JSON.stringify(user));
  }
}
