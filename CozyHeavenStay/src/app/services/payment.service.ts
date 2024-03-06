import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { Payment } from '../models/payment.Model';
import { paymentEndpoints } from './apis';

@Injectable({
  providedIn: 'root'
})
export class PaymentService {
  
  constructor(private http: HttpClient) { }

  getAllPayments(): Observable<Payment[]> { 
    return this.http.get<Payment[]>(paymentEndpoints.GET_ALL_PAYMENTS_API)
      .pipe(
        catchError(this.handleError)
      );
  }

  getPaymentById(id: number): Observable<Payment> { 
    return this.http.get<Payment>(`${paymentEndpoints.GET_PAYMENT_BY_ID_API}/${id}`)
      .pipe(
        catchError(this.handleError)
      );
  }

  getPaymentByBookingId(id: number): Observable<Payment> { 
    return this.http.get<Payment>(`${paymentEndpoints.GET_PAYMENT_BY_BOOKING_ID_API}/${id}`)
      .pipe(
        catchError(this.handleError)
      );
  }

  createPayment(payment: Payment): Observable<Payment> { 
    return this.http.post<Payment>(paymentEndpoints.CREATE_PAYMENT_API, payment)
      .pipe(
        catchError(this.handleError)
      );
  }

  updatePayment(payment: Payment): Observable<any> { 
    return this.http.put<any>(paymentEndpoints.UPDATE_PAYMENT_API, payment)
      .pipe(
        catchError(this.handleError)
      );
  }

  deletePayment(id: number): Observable<any> {
    return this.http.delete<any>(`${paymentEndpoints.DELETE_PAYMENT_API}/${id}`)
      .pipe(
        catchError(this.handleError)
      );
  }

  private handleError(error: any) {
    console.error('API Error:', error);
    return throwError('Something went wrong; please try again later.');
  }
}
