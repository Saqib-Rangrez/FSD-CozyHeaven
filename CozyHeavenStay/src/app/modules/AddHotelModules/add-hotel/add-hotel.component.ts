import { Component, inject } from '@angular/core';
import { HotelService } from '../../../services/hotel.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-add-hotel',
  templateUrl: './add-hotel.component.html',
  styleUrl: './add-hotel.component.css'
})
export class AddHotelComponent {
  step;
  hoteService : HotelService = inject(HotelService);
  router : Router = inject(Router);

  moveToListing() {
    this.router.navigate(['/dashboard/my-listings']);
  }


  ngDoCheck() {
    this.step = this.hoteService.step;
    console.log(this.step);
  }

  next() {
    this.hoteService.step++;
    console.log(this.step)
  }

  previous() {
    this.hoteService.step--;
    console.log(this.step)
  }
}
