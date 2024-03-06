import { Component, inject } from '@angular/core';
import { Booking } from '../../../models/booking.Model';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { HotelService } from '../../../services/hotel.service';
import { Hotel } from '../../../models/hotel.Model';

@Component({
  selector: 'app-hotel',
  templateUrl: './hotel.component.html',
  styleUrl: './hotel.component.css'
})
export class HotelComponent {
  bookingForm: FormGroup;
  hotelService : HotelService = inject(HotelService);
  hotelList;
  ngOnInit(): void {
    this.bookingForm = new FormGroup({
      location: new FormControl ('', [Validators.required]),
      checkInDate: new FormControl (null,[ Validators.required]),
      checkOutDate: new FormControl (null, [Validators.required]),
      numberOfRooms: new FormControl (0, [Validators.required]),
      numberOfAdults: new FormControl (0, [ Validators.required]),
      numberOfChildren: new FormControl (0, [Validators.required]) 
    });

    this.hotelService.getAllHotels().subscribe(
      (res) => {
        this.hotelList = res;
        console.log(this.hotelList)
        console.log("api response: " + res);
      },
      error => {
        console.log(error);
      }
    );
  }

  onSubmit() {
    // Handle form submission
    const bookingData: Booking = this.bookingForm.value;
    console.log(bookingData);
    this.hotelService.searchHotels(bookingData).subscribe({
      next : (res) => {
        console.log(res);
        this.hotelList = res;
      },
      error : (err) => {
        console.log(err);
      }
    })          
  }


  addAdult(data){
    if(data) {
      this.bookingForm.patchValue({
        numberOfAdults: +this.bookingForm.value.numberOfAdults + 1
      })
    }else{
      if(this.bookingForm.value.numberOfAdults > 0) {
        this.bookingForm.patchValue({
          numberOfAdults: +this.bookingForm.value.numberOfAdults - 1
        })
      }      
    }    
  }

  noOfChildren(data) {
    if(data) {
      this.bookingForm.patchValue({
        numberOfChildren: +this.bookingForm.value.numberOfChildren + 1
      })
    }else{
      if(this.bookingForm.value.numberOfChildren > 0){
        this.bookingForm.patchValue({
          numberOfChildren: +this.bookingForm.value.numberOfChildren - 1
        })
      }
      
    }    
  }

  noOfRooms(data) {
    if(data) {
      if(this.bookingForm.value.numberOfRooms <  8){
        this.bookingForm.patchValue({
          numberOfRooms: +this.bookingForm.value.numberOfRooms + 1
        })
      }      
    }else{
      if(this.bookingForm.value.numberOfRooms > 0){
        this.bookingForm.patchValue({
          numberOfRooms: +this.bookingForm.value.numberOfRooms - 1
        })
      }
      
    }    
  }
}
