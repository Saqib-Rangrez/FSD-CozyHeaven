import { Component, inject } from '@angular/core';
import { Booking } from '../../../models/booking.Model';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { HotelService } from '../../../services/hotel.service';
import { Hotel } from '../../../models/hotel.Model';
import { SearchHotelDTO } from '../../../models/DTO/search-hotel-dto.Model';

@Component({
  selector: 'app-hotel',
  templateUrl: './hotel.component.html',
  styleUrl: './hotel.component.css'
})
export class HotelComponent {
  searchForm: FormGroup;
  hotelService : HotelService = inject(HotelService);
  hotelList;
  minDate: Date; 
  user;

  ngOnInit(): void {
    this.searchForm = new FormGroup({
      location: new FormControl ('', [Validators.required]),
      selectedDates: new FormControl ([], [Validators.required]),
      numberOfRooms: new FormControl (0, [Validators.required]),
      numberOfAdults: new FormControl (0, [ Validators.required]),
      numberOfChildren: new FormControl (0, [Validators.required]) 
    });
    this.minDate = new Date();

    this.user = JSON.parse(localStorage.getItem('user'));

    this.hotelService.getAllHotels(this.user.token).subscribe(
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
    const searchData: SearchHotelDTO = new SearchHotelDTO(
      this.searchForm.get('location').value,
      this.searchForm.get('selectedDates').value[0],
      this.searchForm.get('selectedDates').value[1],
      this.searchForm.get('numberOfRooms').value,
      this.searchForm.get('numberOfAdults').value,
      this.searchForm.get('numberOfChildren').value
    );
    console.log(searchData);
    this.hotelService.searchHotels(searchData,this.user.token).subscribe({
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
      this.searchForm.patchValue({
        numberOfAdults: +this.searchForm.value.numberOfAdults + 1
      })
    }else{
      if(this.searchForm.value.numberOfAdults > 0) {
        this.searchForm.patchValue({
          numberOfAdults: +this.searchForm.value.numberOfAdults - 1
        })
      }      
    }    
  }

  noOfChildren(data) {
    if(data) {
      this.searchForm.patchValue({
        numberOfChildren: +this.searchForm.value.numberOfChildren + 1
      })
    }else{
      if(this.searchForm.value.numberOfChildren > 0){
        this.searchForm.patchValue({
          numberOfChildren: +this.searchForm.value.numberOfChildren - 1
        })
      }
      
    }    
  }

  noOfRooms(data) {
    if(data) {
      if(this.searchForm.value.numberOfRooms <  8){
        this.searchForm.patchValue({
          numberOfRooms: +this.searchForm.value.numberOfRooms + 1
        })
      }      
    }else{
      if(this.searchForm.value.numberOfRooms > 0){
        this.searchForm.patchValue({
          numberOfRooms: +this.searchForm.value.numberOfRooms - 1
        })
      }
      
    }    
  }
}
