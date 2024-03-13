import { Component, inject } from '@angular/core';
import { BookingService } from '../../../../../services/booking.service';
import { ToastrService } from 'ngx-toastr';
import { NavigationEnd, Router } from '@angular/router';

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
  router : Router = inject(Router);
  toastr : ToastrService = inject(ToastrService);
  bookingService : BookingService = inject(BookingService);

  ngOnInit() {
    this.user = JSON.parse(localStorage.getItem('user'));
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
        this.filterHotel('Pending');
        this.loading = false;
        if(this.user.role !== 'Admin') {
          this.bookings = this.filterBookingsForOwner();
        }
      }
    })

    this.router.events.subscribe((event) => {
      if (event instanceof NavigationEnd) {
        window.scrollTo(0, 0);
      }
    });  
  }

  filterBookingsForOwner() {
    return this.bookings = this.bookings.filter(booking => {
      if(this.user.role === 'Owner'){
        return booking.hotel.ownerId === this.user.userId;
      }else{
        return booking.userId === this.user.userId;
      }
    })
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
