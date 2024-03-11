import { Injectable, inject } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError, finalize, tap } from 'rxjs/operators';
import { HotelOwner } from '../models/hotel-owner.Model';
import { ownerEndpoints } from './apis'; 
import { User } from '../models/User.Model';
import { ToastrService } from 'ngx-toastr';

@Injectable({
  providedIn: 'root'
})
export class HotelOwnerService {
  toastr : ToastrService = inject(ToastrService);
  
  constructor(private http: HttpClient) { }

  setToken(token: string ){
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        'Authorization': `Bearer ${token}` 
      })
    };
    return httpOptions;
  }

  getAllHotelOwners(token: string ): Observable<HotelOwner[]> { 
    return this.http.get<HotelOwner[]>(ownerEndpoints.GET_ALL_HOTEL_OWNERS_API,this.setToken(token))
      .pipe(
        catchError(this.handleError)
      );
  }

  getHotelOwnerById(id: number,token: string ): Observable<any> { 
    return this.http.get<any>(`${ownerEndpoints.GET_HOTEL_OWNER_BY_ID_API}${id}`,this.setToken(token))
      .pipe(
        catchError(this.handleError)
      );
  }

  createHotelOwner(hotelOwner: HotelOwner ): Observable<HotelOwner> { 
    return this.http.post<HotelOwner>(ownerEndpoints.CREATE_HOTEL_OWNER_API, hotelOwner)
      .pipe(
        catchError(this.handleError)
      );
  }

  updateHotelOwner(hotelOwner: HotelOwner,token: string ): Observable<any> { 
    const loadingToast = this.toastr.info('Updating User...', 'Please wait', {
      disableTimeOut: true,
      closeButton: false,
      positionClass: 'toast-top-center'
    });

    return this.http.put<any>(ownerEndpoints.UPDATE_HOTEL_OWNER_API, hotelOwner,this.setToken(token))
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


  deleteHotelOwner(id: number,token: string): Observable<any> {
    console.log(id)
    return this.http.delete<any>(`${ownerEndpoints.DELETE_HOTEL_OWNER_API}${id}`,this.setToken(token))
      .pipe(
        catchError(this.handleError)
      );
  }


  private handleCreateUser(res){
    const expiresInTs = new Date().getTime() + ((3 * 60 * 60) * 1000);
    const expiresIn = new Date(expiresInTs);
    let user : any;
    console.log("from services handle...." ,res)

    // if(res.user.role == "Admin") {
    //   user = new Admin(
    //     res.user.adminId,
    //     res.user.firstName,
    //     res.user.lastName,
    //     res.user.email,
    //     res.user.password,
    //     res.user.profileImage,
    //     res.user.role,
    //     res.user.token,
    //     res.user.resetPasswordExpires,
    //     //expiresIn
    //   )

    //  }
    if(res.user.role == "Owner"){
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
      }

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

  private handleError(error: any) {
    console.error('API Error:', error);
    return throwError('Something went wrong; please try again later.');
  }
}
