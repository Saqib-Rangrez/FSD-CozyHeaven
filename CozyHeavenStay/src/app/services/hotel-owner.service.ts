import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { HotelOwner } from '../models/hotel-owner.Model';
import { ownerEndpoints } from './apis'; 

@Injectable({
  providedIn: 'root'
})
export class HotelOwnerService {
  
  constructor(private http: HttpClient) { }

  getAllHotelOwners(): Observable<HotelOwner[]> { 
    return this.http.get<HotelOwner[]>(ownerEndpoints.GET_ALL_HOTEL_OWNERS_API)
      .pipe(
        catchError(this.handleError)
      );
  }

  getHotelOwnerById(id: number): Observable<HotelOwner> { 
    return this.http.get<HotelOwner>(`${ownerEndpoints.GET_HOTEL_OWNER_BY_ID_API}/${id}`)
      .pipe(
        catchError(this.handleError)
      );
  }

  createHotelOwner(hotelOwner: HotelOwner): Observable<HotelOwner> { 
    return this.http.post<HotelOwner>(ownerEndpoints.CREATE_HOTEL_OWNER_API, hotelOwner)
      .pipe(
        catchError(this.handleError)
      );
  }

  updateHotelOwner(hotelOwner: HotelOwner): Observable<any> { 
    return this.http.put<any>(ownerEndpoints.UPDATE_HOTEL_OWNER_API, hotelOwner)
      .pipe(
        catchError(this.handleError)
      );
  }

  deleteHotelOwner(id: number): Observable<any> {
    return this.http.delete<any>(`${ownerEndpoints.DELETE_HOTEL_OWNER_API}/${id}`)
      .pipe(
        catchError(this.handleError)
      );
  }

  private handleError(error: any) {
    console.error('API Error:', error);
    return throwError('Something went wrong; please try again later.');
  }
}
