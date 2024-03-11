import { Component, Input, inject } from '@angular/core';
import { HotelService } from '../../../services/hotel.service';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-add-hotel',
  templateUrl: './add-hotel.component.html',
  styleUrl: './add-hotel.component.css'
})
export class AddHotelComponent {
  step;
  @Input() editData;
  hoteService : HotelService = inject(HotelService);
  router : Router = inject(Router);
  activatedRoute: ActivatedRoute = inject(ActivatedRoute);
  id ;

  moveToListing() {
    this.router.navigate(['/dashboard/my-listings']);
    this.hoteService.step = 1;
  }

  ngOnInit() {
    if (this.activatedRoute.snapshot && this.activatedRoute.snapshot.paramMap) {
      this.id = this.activatedRoute.snapshot.paramMap.get('id');
    }
    console.log(this.id);
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
