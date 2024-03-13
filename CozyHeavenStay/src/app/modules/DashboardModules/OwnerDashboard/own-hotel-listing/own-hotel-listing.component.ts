import { Component, inject } from '@angular/core';
import { HotelOwnerService } from '../../../../services/hotel-owner.service';

@Component({
  selector: 'app-own-hotel-listing',
  templateUrl: './own-hotel-listing.component.html',
  styleUrl: './own-hotel-listing.component.css'
})
export class OwnHotelListingComponent {
  ownerService : HotelOwnerService = inject(HotelOwnerService)
  user : any;
  hotelList;
  loading : boolean = false;

  ngOnInit() {
    this.user = JSON.parse(localStorage.getItem('user'));
    this.loading = true;
    this.ownerService.getHotelOwnerById(this.user?.userId,this.user.token).subscribe({
      next : res => {
        this.hotelList = res.data.hotels;
      },
      error : err =>{
        this.loading = false;
      },
      complete : () => {
        this.loading = false;
      }
    });
  }

  onDelete(value) {
    this.ngOnInit();
  }

}
