import { Component, inject } from '@angular/core';
import { ActivatedRoute} from '@angular/router';
import { BookingService } from '../../../services/booking.service';
import { Booking } from '../../../models/booking.Model';
import { ToastrService } from 'ngx-toastr';
import { User } from '../../../models/User.Model';

@Component({
  selector: 'app-booking-confirm',
  templateUrl: './booking-confirm.component.html',
  styleUrl: './booking-confirm.component.css'
})
export class BookingConfirmComponent {

  bookingId:number;
  response;
  user: any;
  booking: Booking;
  toastr : ToastrService = inject(ToastrService); 

  constructor( 
    private activatedRoute: ActivatedRoute,
    private bookingService: BookingService,
    
    ){}

  ngOnInit():void{
    

    this.bookingId = this.activatedRoute.snapshot.params['bookingid'];

    this.user = JSON.parse(localStorage.getItem("user"))
    this.bookingService.getBookingById(this.bookingId,this.user.token).subscribe({
      next : (res) => {
      
        this.response = res;
        this.booking = this.response.data;
      },
      error : (err) => {
        console.log(err);
      }
    }) 

  }

  printPdf() {

    window.print();
    this.toastr.success("Downloaded !!");

  }
}
