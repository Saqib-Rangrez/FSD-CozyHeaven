import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { Refund } from '../models/refund.Model';
import { refundEndpoints } from './apis';

@Injectable({
  providedIn: 'root'
})
export class RefundService {

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

  getAllRefunds(token: string): Observable<Refund[]> {
    return this.http.get<Refund[]>(refundEndpoints.GET_ALL_REFUNDS_API,this.setToken(token))
      .pipe(
        catchError(this.handleError)
      );
  }

  getRefundById(id: number,token: string): Observable<Refund> {
    return this.http.get<Refund>(`${refundEndpoints.GET_REFUND_BY_ID_API}${id}`,this.setToken(token))
      .pipe(
        catchError(this.handleError)
      );
  }

  getRefundByPaymentId(paymentId: number,token: string): Observable<Refund> {
    return this.http.get<Refund>(`${refundEndpoints.GET_REFUND_BY_PAYMENT_ID_API}${paymentId}`,this.setToken(token))
      .pipe(
        catchError(this.handleError)
      );
  }

  createRefund(refund: Refund,token: string): Observable<Refund> {
    return this.http.post<Refund>(refundEndpoints.CREATE_REFUND_API, refund,this.setToken(token))
      .pipe(
        catchError(this.handleError)
      );
  }

  updateRefund(refund: Refund,token: string): Observable<any> {
    return this.http.put<any>(refundEndpoints.UPDATE_REFUND_API, refund,this.setToken(token))
      .pipe(
        catchError(this.handleError)
      );
  }

  deleteRefund(id: number,token: string): Observable<any> {
    return this.http.delete<any>(`${refundEndpoints.DELETE_REFUND_API}${id}`,this.setToken(token))
      .pipe(
        catchError(this.handleError)
      );
  }

  approveRefund(refundId: number,token: string): Observable<any> {
    return this.http.post<any>(`${refundEndpoints.APPROVE_REFUND_API}${refundId}`, this.setToken(token))
      .pipe(
        catchError(this.handleError)
      );
  }

  private handleError(error: any) {
    console.error('API Error:', error);
    return throwError('Something went wrong; please try again later.');
  }
}
