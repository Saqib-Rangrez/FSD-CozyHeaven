import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { Refund } from '../models/refund.Model';
import { refundEndpoints } from './apis';

@Injectable({
  providedIn: 'root'
})
export class RefundService {

  constructor(private http: HttpClient) { }

  getAllRefunds(): Observable<Refund[]> {
    return this.http.get<Refund[]>(refundEndpoints.GET_ALL_REFUNDS_API)
      .pipe(
        catchError(this.handleError)
      );
  }

  getRefundById(id: number): Observable<Refund> {
    return this.http.get<Refund>(`${refundEndpoints.GET_REFUND_BY_ID_API}${id}`)
      .pipe(
        catchError(this.handleError)
      );
  }

  getRefundByPaymentId(paymentId: number): Observable<Refund> {
    return this.http.get<Refund>(`${refundEndpoints.GET_REFUND_BY_PAYMENT_ID_API}${paymentId}`)
      .pipe(
        catchError(this.handleError)
      );
  }

  createRefund(refund: Refund): Observable<Refund> {
    return this.http.post<Refund>(refundEndpoints.CREATE_REFUND_API, refund)
      .pipe(
        catchError(this.handleError)
      );
  }

  updateRefund(refund: Refund): Observable<any> {
    return this.http.put<any>(refundEndpoints.UPDATE_REFUND_API, refund)
      .pipe(
        catchError(this.handleError)
      );
  }

  deleteRefund(id: number): Observable<any> {
    return this.http.delete<any>(`${refundEndpoints.DELETE_REFUND_API}${id}`)
      .pipe(
        catchError(this.handleError)
      );
  }

  approveRefund(refundId: number): Observable<any> {
    return this.http.post<any>(`${refundEndpoints.APPROVE_REFUND_API}${refundId}`, {})
      .pipe(
        catchError(this.handleError)
      );
  }

  private handleError(error: any) {
    console.error('API Error:', error);
    return throwError('Something went wrong; please try again later.');
  }
}
