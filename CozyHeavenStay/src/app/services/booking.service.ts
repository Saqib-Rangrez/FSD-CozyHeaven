import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { Booking } from '../models/booking.Model';
import { bookingsEndpoints } from './apis';

@Injectable({
  providedIn: 'root'
})
export class BookingService {
  
  constructor(private http: HttpClient) { }

  getAllBookings(): Observable<Booking[]> { 
    return this.http.get<Booking[]>(bookingsEndpoints.GET_ALL_BOOKING_API)
      .pipe(
        catchError(this.handleError)
      );
  }

  getBookingById(id: number): Observable<Booking> { 
    return this.http.get<Booking>(`${bookingsEndpoints.GET_BOOKING_BY_BOOKINGID_API}/${id}`)
      .pipe(
        catchError(this.handleError)
      );
  }

  createBooking(booking: Booking): Observable<Booking> { 
    return this.http.post<Booking>(bookingsEndpoints.ADD_BOOKING_API, booking)
      .pipe(
        catchError(this.handleError)
      );
  }

  updateBooking(booking: Booking): Observable<any> { 
    return this.http.put<any>(bookingsEndpoints.UPDATE_BOOKINGBOOKING_API, booking)
      .pipe(
        catchError(this.handleError)
      );
  }

  deleteBooking(id: number): Observable<any> {
    return this.http.delete<any>(`${bookingsEndpoints.DELETE_BOOKING_API}/${id}`)
      .pipe(
        catchError(this.handleError)
      );
  }

  private handleError(error: any) {
    console.error('API Error:', error);
    return throwError('Something went wrong; please try again later.');
  }
}
