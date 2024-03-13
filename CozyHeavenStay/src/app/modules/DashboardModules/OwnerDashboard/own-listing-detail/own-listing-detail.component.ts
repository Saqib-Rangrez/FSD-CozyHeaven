import { Component, EventEmitter, Input, Output, ViewChild, inject } from '@angular/core';
import { HotelService } from '../../../../services/hotel.service';
import { ToastrService } from 'ngx-toastr';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-own-listing-detail',
  templateUrl: './own-listing-detail.component.html',
  styleUrl: './own-listing-detail.component.css'
})
export class OwnListingDetailComponent {
@Input() data;
hotelService : HotelService = inject(HotelService);
toaster : ToastrService = inject(ToastrService);
router : Router = inject(Router);
activatedRoute : ActivatedRoute = inject(ActivatedRoute);
owner ;
@Output() dataDeleted: EventEmitter<boolean> = new EventEmitter<boolean>();


deleteHotel(id:number) {

  this.owner = JSON.parse(localStorage.getItem('user'));
  this.hotelService.deleteHotel(id,this.owner.token).subscribe({
    next: data => {
      this.toaster.success('Hotel deleted successfully');
      this.router.navigate(['/dashboard/my-listings']);
    },
    error: error => {
      this.toaster.error('Something went wrong');
    },
    complete : () => {
      this.dataDeleted.emit(true);
    }
  })
}

editHotel() {
  this.router.navigate(['/add-hotel']);
  this.hotelService.edit = true;
}

}
