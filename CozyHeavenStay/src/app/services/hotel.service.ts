import { Injectable, inject } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError, finalize, tap } from 'rxjs/operators';
import { Hotel } from '../models/hotel.Model';
import { hotelEndpoints } from './apis';
import { SearchHotelDTO } from '../models/DTO/search-hotel-dto.Model';
import { HotelDTO } from '../models/DTO/HotelDTO.Model';
import { ToastrService } from 'ngx-toastr';

@Injectable({
  providedIn: 'root'
})
export class HotelService {
  toastr : ToastrService = inject(ToastrService);
  hotelInfo;
  roomData;
  edit : boolean = false;
  step : number = 1;

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

  getAllHotels(token: string) { 
    return this.http.get(hotelEndpoints.GET_ALL_HOTELS_API,this.setToken(token))
      .pipe(
        catchError(this.handleError)
      );
  }

  getHotelById(id: number,token: string): Observable<any> { 
    return this.http.get<any>(`${hotelEndpoints.GET_HOTEL_BY_ID_API}${id}`,this.setToken(token))
      .pipe(
        catchError(this.handleError)
      );
  }

  getHotelByName(name: string,token: string): Observable<Hotel> { 
    return this.http.get<Hotel>(`${hotelEndpoints.GET_HOTEL_BY_NAME_API}${name}`,this.setToken(token))
      .pipe(
        catchError(this.handleError)
      );
  }

  createHotel(formData,token: string): Observable<any> { 
    const loadingToast = this.toastr.info('Adding Hotel...', 'Please wait', {
      disableTimeOut: true,
      closeButton: false,
      positionClass: 'toast-top-center'
    });

    console.log("TOken Hotel", token);

    const httpOptions = {
      headers: new HttpHeaders({
        'Authorization': `Bearer ${token}` 
      })
    };

    return this.http.post<any>(hotelEndpoints.CREATE_HOTEL_API, formData, httpOptions)
      .pipe(
        catchError(error => {
          if (loadingToast) {
            this.toastr.clear();
          }
          this.toastr.error('Failed to sign up', 'Error');
          
          return throwError(() => error);
        }),        
        tap((res) => {
          console.log(res);
          if(loadingToast){
            this.toastr.clear();
          }
          this.hotelInfo = res.data;
        }),
        finalize(() => {
          if (loadingToast) {
            this.toastr.clear();
          }
        }),         
      );
  }

  updateHotel(hotel: Hotel,token: string): Observable<any> { 
    return this.http.put<any>(hotelEndpoints.UPDATE_HOTEL_API, hotel, this.setToken(token))
      .pipe(
        catchError(this.handleError)
      );
  }

  deleteHotel(id: number,token: string): Observable<any> {
    return this.http.delete<any>(`${hotelEndpoints.DELETE_HOTEL_API}${id}`, this.setToken(token))
      .pipe(
        catchError(this.handleError)
      );
  }

  searchHotels(searchHotelDTO: SearchHotelDTO,token: string): Observable<Hotel[]> { 
    return this.http.post<Hotel[]>(hotelEndpoints.SEARCH_HOTELS_API, searchHotelDTO, this.setToken(token))
      .pipe(
        catchError(this.handleError)
      );
  }

  private handleError(error: any) {
    console.error('API Error:', error);
    return throwError('Something went wrong; please try again later.');
  }
}
