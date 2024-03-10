import { Component, EventEmitter, Input, Output, inject } from '@angular/core';
import { BookingService } from '../../../../../services/booking.service';
import { ToastrService } from 'ngx-toastr';
import { Booking } from '../../../../../models/booking.Model';
import { Router } from '@angular/router';

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
  @Output() booleanValue = new EventEmitter<boolean>();

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
      console.log(res);
      this.toaster.success("Booking Cancelled Successfully");
      this.booleanValue.emit(true);
    },
    error : err => {
      console.log(err);
      this.toaster.error("Something went wrong");
    },
    complete : () => {
      
    }
  });
 }
}
