import { Component, inject } from '@angular/core';
import { BookingService } from '../../../../../services/booking.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-ad-bookings',
  templateUrl: './ad-bookings.component.html',
  styleUrl: './ad-bookings.component.css'
})
export class AdBookingsComponent {
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
    this.bookingService.getAllBookings(this.user.token).subscribe({
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

  handleReload(value) {
    if(value){
      this.ngOnInit();
    }
  }

  filterHotel(key : string) {
    this.filterBookings = this.bookings.filter((val) => val?.status?.toLocaleLowerCase() === key.toLocaleLowerCase() );
  }
}
