import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { Payment } from '../models/payment.Model';
import { paymentEndpoints } from './apis';

@Injectable({
  providedIn: 'root'
})
export class PaymentService {
  
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
  
  getAllPayments(token: string): Observable<any> { 
    return this.http.get<any>(paymentEndpoints.GET_ALL_PAYMENTS_API,this.setToken(token))
      .pipe(
        catchError(this.handleError)
      );
  }

  getPaymentById(id: number,token: string): Observable<any> { 
    return this.http.get<any>(`${paymentEndpoints.GET_PAYMENT_BY_ID_API}${id}`,this.setToken(token))
      .pipe(
        catchError(this.handleError)
      );
  }

  getPaymentByBookingId(id: number,token: string): Observable<Payment> { 
    return this.http.get<Payment>(`${paymentEndpoints.GET_PAYMENT_BY_BOOKING_ID_API}${id}`,this.setToken(token))
      .pipe(
        catchError(this.handleError)
      );
  }

  createPayment(payment: Payment,token: string): Observable<Payment> { 
    return this.http.post<Payment>(paymentEndpoints.CREATE_PAYMENT_API, payment,this.setToken(token))
      .pipe(
        catchError(this.handleError)
      );
  }

  updatePayment(payment: Payment,token: string): Observable<any> { 
    return this.http.put<any>(paymentEndpoints.UPDATE_PAYMENT_API, payment,this.setToken(token))
      .pipe(
        catchError(this.handleError)
      );
  }

  deletePayment(id: number,token: string): Observable<any> {
    return this.http.delete<any>(`${paymentEndpoints.DELETE_PAYMENT_API}${id}`,this.setToken(token))
      .pipe(
        catchError(this.handleError)
      );
  }

  private handleError(error: any) {
    console.error('API Error:', error);
    return throwError('Something went wrong; please try again later.');
  }
}
