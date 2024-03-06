import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { Hotel } from '../models/hotel.Model';
import { hotelEndpoints } from './apis';
import { SearchHotelDTO } from '../models/DTO/search-hotel-dto.Model';

@Injectable({
  providedIn: 'root'
})
export class HotelService {

  constructor(private http: HttpClient) { }

  getAllHotels(): Observable<Hotel[]> { 
    return this.http.get<Hotel[]>(hotelEndpoints.GET_ALL_HOTELS_API)
      .pipe(
        catchError(this.handleError)
      );
  }

  getHotelById(id: number): Observable<Hotel> { 
    return this.http.get<Hotel>(`${hotelEndpoints.GET_HOTEL_BY_ID_API}/${id}`)
      .pipe(
        catchError(this.handleError)
      );
  }

  getHotelByName(name: string): Observable<Hotel> { 
    return this.http.get<Hotel>(`${hotelEndpoints.GET_HOTEL_BY_NAME_API}/${name}`)
      .pipe(
        catchError(this.handleError)
      );
  }

  createHotel(hotel: Hotel): Observable<Hotel> { 
    return this.http.post<Hotel>(hotelEndpoints.CREATE_HOTEL_API, hotel)
      .pipe(
        catchError(this.handleError)
      );
  }

  updateHotel(hotel: Hotel): Observable<any> { 
    return this.http.put<any>(hotelEndpoints.UPDATE_HOTEL_API, hotel)
      .pipe(
        catchError(this.handleError)
      );
  }

  deleteHotel(id: number): Observable<any> {
    return this.http.delete<any>(`${hotelEndpoints.DELETE_HOTEL_API}/${id}`)
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
