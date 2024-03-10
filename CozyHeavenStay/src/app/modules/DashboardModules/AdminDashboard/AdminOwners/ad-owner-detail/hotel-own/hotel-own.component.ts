import { Component, Input, inject } from '@angular/core';
import { HotelService } from '../../../../../../services/hotel.service';
import { ToastrService } from 'ngx-toastr';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-hotel-own',
  templateUrl: './hotel-own.component.html',
  styleUrl: './hotel-own.component.css'
})
export class HotelOwnComponent {
@Input() data;
hotelService : HotelService = inject(HotelService);
toaster : ToastrService = inject(ToastrService);
router : Router = inject(Router);
activatedRoute : ActivatedRoute = inject(ActivatedRoute);
ownerId : number;

deleteHotel(id:number) {

  this.activatedRoute.params.subscribe(params => {
    this.ownerId= params['id'];
  });

  this.hotelService.deleteHotel(id).subscribe({
    next: data => {
      this.toaster.success('Hotel deleted successfully');
      console.log(data)
      this.router.navigate(['/manage-owners/detail/'+ this.ownerId]);
    },
    error: error => {
      this.toaster.error('Something went wrong');
    }
  })
}
}
