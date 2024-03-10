import { Component, inject } from '@angular/core';
import { ActivatedRoute} from '@angular/router';
import { BookingService } from '../../../services/booking.service';
import { Booking } from '../../../models/booking.Model';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-booking-confirm',
  templateUrl: './booking-confirm.component.html',
  styleUrl: './booking-confirm.component.css'
})
export class BookingConfirmComponent {

  bookingId:number;
  response;
  booking: Booking;
  toastr : ToastrService = inject(ToastrService); 

  constructor( 
    private activatedRoute: ActivatedRoute,
    private bookingService: BookingService,
    
    ){}

  ngOnInit():void{
    

    this.bookingId = this.activatedRoute.snapshot.params['bookingid'];
    console.log(this.bookingId);


    this.bookingService.getBookingById(this.bookingId).subscribe({
      next : (res) => {
      
        this.response = res;
        this.booking = this.response.data;
        console.log("book",this.booking);
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
