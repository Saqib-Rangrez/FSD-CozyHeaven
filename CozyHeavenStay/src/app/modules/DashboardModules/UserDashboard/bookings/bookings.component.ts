import { Component, inject } from '@angular/core';
import { Booking } from '../../../../models/booking.Model';
import { ToastrService } from 'ngx-toastr';
import { BookingService } from '../../../../services/booking.service';

@Component({
  selector: 'app-bookings',
  templateUrl: './bookings.component.html',
  styleUrl: './bookings.component.css'
})
export class BookingsComponent {
  bookings;
  filterBookings;
  user : any;
  loading : boolean;
  toastr : ToastrService = inject(ToastrService);
  bookingService : BookingService = inject(BookingService);

  ngOnInit() {
    this.user = JSON.parse(localStorage.getItem('user'));
    console.log(this.user)
    this.loading = true;
    this.bookingService.getBookingByUserId(this.user.userId).subscribe({
      next : res => {
        this.bookings = res.data;        
        this.toastr.success("Data Fetched Successfully");
      },
      error : err => {
        console.log(err);
        this.toastr.success("No bookings found");
      },
      complete : () => {
        console.log(this.bookings);
        this.filterHotel('Pending');
        console.log(this.filterBookings);
        this.loading = false;
      }
    })

    console.log(this.bookings)
  }

  filterHotel(key : string) {
    this.filterBookings = this.bookings.filter((val) => val?.status?.toLocaleLowerCase() === key.toLocaleLowerCase() );
  }

}
