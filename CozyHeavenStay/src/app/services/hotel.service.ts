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
  edit : boolean = false;
  step : number = 1;

  constructor(private http: HttpClient) { }

  getAllHotels() { 
    return this.http.get(hotelEndpoints.GET_ALL_HOTELS_API)
      .pipe(
        catchError(this.handleError)
      );
  }

  getHotelById(id: number): Observable<Hotel> { 
    return this.http.get<Hotel>(`${hotelEndpoints.GET_HOTEL_BY_ID_API}${id}`)
      .pipe(
        catchError(this.handleError)
      );
  }

  getHotelByName(name: string): Observable<Hotel> { 
    return this.http.get<Hotel>(`${hotelEndpoints.GET_HOTEL_BY_NAME_API}${name}`)
      .pipe(
        catchError(this.handleError)
      );
  }

  createHotel(formData): Observable<any> { 
    const loadingToast = this.toastr.info('Signing up...', 'Please wait', {
      disableTimeOut: true,
      closeButton: false,
      positionClass: 'toast-top-center'
    });

    const headers = new HttpHeaders();
    headers.append('Content-Type', 'multipart/form-data');

    return this.http.post<any>(hotelEndpoints.CREATE_HOTEL_API, formData,{ headers: headers })
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
        }),
        finalize(() => {
          if (loadingToast) {
            this.toastr.clear();
          }
        }),         
      );
  }

  updateHotel(hotel: Hotel): Observable<any> { 
    return this.http.put<any>(hotelEndpoints.UPDATE_HOTEL_API, hotel)
      .pipe(
        catchError(this.handleError)
      );
  }

  deleteHotel(id: number): Observable<any> {
    return this.http.delete<any>(`${hotelEndpoints.DELETE_HOTEL_API}${id}`)
      .pipe(
        catchError(this.handleError)
      );
  }

  searchHotels(searchHotelDTO: SearchHotelDTO): Observable<Hotel[]> { 
    return this.http.post<Hotel[]>(hotelEndpoints.SEARCH_HOTELS_API, searchHotelDTO)
      .pipe(
        catchError(this.handleError)
      );
  }

  private handleError(error: any) {
    console.error('API Error:', error);
    return throwError('Something went wrong; please try again later.');
  }
}
