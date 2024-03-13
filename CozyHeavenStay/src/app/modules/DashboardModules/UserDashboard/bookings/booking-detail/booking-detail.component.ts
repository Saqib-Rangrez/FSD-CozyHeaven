import { Component, EventEmitter, Input, Output, inject } from '@angular/core';
import { BookingService } from '../../../../../services/booking.service';
import { ToastrService } from 'ngx-toastr';
import { Booking } from '../../../../../models/booking.Model';
import { Router } from '@angular/router';
import { Refund } from '../../../../../models/refund.Model';
import { RefundService } from '../../../../../services/refund.service';
import { PaymentService } from '../../../../../services/payment.service';
import { Payment } from '../../../../../models/payment.Model';

@Component({
  selector: 'app-booking-detail',
  templateUrl: './booking-detail.component.html',
  styleUrl: './booking-detail.component.css'
})
export class BookingDetailComponent {
 @Input() data : any;
  user : any;
  bookingService : BookingService = inject(BookingService);
  toaster : ToastrService = inject(ToastrService);
  router : Router = inject(Router);
  refundService : RefundService = inject(RefundService);
  paymentService : PaymentService = inject(PaymentService);
  @Output() booleanValue = new EventEmitter<boolean>();
  refundResult;

 ngOnInit() {
  this.user = JSON.parse(localStorage.getItem('user'));
 }

 cancelBooking() {
  const cancelData : Booking = new Booking(
    this.data.bookingId,
    this.data.numberOfGuests,
    this.data.checkInDate,
    this.data.checkOutDate,
    this.data.totalFare,
    this.data.status,
    this.data.userId,
    this.data.roomId,
    this.data.hotelId,
    this.data.paymentId,
  );


  cancelData.status = 'Canceled';
  this.bookingService.updateBooking(cancelData,this.user.token).subscribe({
    next : res => {
      this.toaster.success("Booking Cancelled Successfully");
      this.booleanValue.emit(true);
      location.reload();
    },
    error : err => {
      console.log(err);
      this.toaster.error("Something went wrong");
    },
  });
 }

 requestRefund(payment){
  const refundData : Refund = new Refund(
    payment.paymentId,
    payment.amount,
    new Date(),
    0,
    'Booking Canceled',
    'Pending',
    null,
  );


  this.refundService.createRefund(refundData,this.user.token).subscribe({
    next : res => {
      this.refundResult = res;
      this.refundResult = this.refundResult.data;
    },
    error : err => {
      console.log(err);
      this.toaster.error("Something went wrong");
    },
    complete : () => {
      const paymentToUpdate = new Payment(
        payment.paymentId,
        payment.bookingId,
        payment.paymentMode,
        'Refunded',
        payment.amount,
        payment.paymentDate,
        this.refundResult.refundId,    
        null,
        null,
      )
    
      this.paymentService.updatePayment(paymentToUpdate,this.user.token).subscribe({
        next : res => {
          this.toaster.success("Refund Requested Successfully");
          this.booleanValue.emit(true);
          location.reload();
        },
        error : err => {
          console.log(err);
          this.toaster.error("Something went wrong");
        }
      });
    }
  });  
 }

 payNow(payment) {
  const paymentToUpdate = new Payment(
    payment.paymentId,
    payment.bookingId,
    'Online',
    'Paid',
    payment.amount,
    payment.paymentDate,
    null,    
    null,
    null,
  )

  this.paymentService.updatePayment(paymentToUpdate,this.user.token).subscribe({
    next : res => {
      this.toaster.success("Payment Successful");
      this.booleanValue.emit(true);
      const bookingData : Booking = new Booking(
        this.data.bookingId,
        this.data.numberOfGuests,
        this.data.checkInDate,
        this.data.checkOutDate,
        this.data.totalFare,
        this.data.status,
        this.data.userId,
        this.data.roomId,
        this.data.hotelId,
        this.data.paymentId,
      );
    
      bookingData.status = 'Confirmed';
      this.bookingService.updateBooking(bookingData,this.user.token).subscribe({
        next : res => {
          this.toaster.success("Booking Cancelled Successfully");
          this.booleanValue.emit(true);
          location.reload();
        },
        error : err => {
          console.log(err);
          this.toaster.error("Something went wrong");
        },
      });
    },
    error : err => {
      console.log(err);
      this.toaster.error("Something went wrong");
    },
    complete : () => {
      this.router.navigate(['/confirm/'+ this.data.hotelId]);
    }
  });
}

 approveRefund(payment){
  const refundData : Refund = new Refund(
    payment.paymentId,
    payment.amount,
    new Date(),
    payment.refundId,
    'Booking Canceled',
    'Approved',
    null,
  );
  let updateRefundResult

  this.refundService.updateRefund(refundData,this.user.token).subscribe({

    next : res => {
      location.reload();
      updateRefundResult = res;
      updateRefundResult = updateRefundResult.data;
    },
    error : err => {
      console.log(err);
      this.toaster.error("Something went wrong");
    },
  });
 }
}
