import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { checkoutEndpoints } from './apis';
@Injectable({
 providedIn: 'root'
})
export class RazorpayService {
 constructor(private http: HttpClient) { }

 createOrder(amount): Observable<any> {
  const headers = new HttpHeaders({ 'Content-Type': 'application/json' });
  return this.http.post(checkoutEndpoints.CREATE_ORDER_API, amount, { headers });
}


 verifyPayment(paymentDetails) : Observable<any> {
  const headers = new HttpHeaders({ 'Content-Type': 'application/json' });
  const data = {
    RazorpayOrderId : paymentDetails.razorpay_order_id,
    RazorpayPaymentId : paymentDetails.razorpay_payment_id,
    RazorpaySignature : paymentDetails.razorpay_signature
  }
  console.log(data);
  

  return this.http.post(checkoutEndpoints.VERIFY_PAYMENT_API, data, {headers});
 }
}