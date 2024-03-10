import { Component, Input, AfterViewInit, ElementRef, ViewChild } from '@angular/core';
// import {NgxTinySliderSettingsInterface} from 'ngx-tiny-slider';

@Component({
  selector: 'app-hotelcard',
  templateUrl: './hotelcard.component.html',
  styleUrl: './hotelcard.component.css'
})
export class HotelcardComponent {
@Input() card;
// tinySliderConfig: NgxTinySliderSettingsInterface; 
avgRating : number = 0;

ngOnInit() {
  this.avgRating = this.GetAvgRating(this.card.reviews);
}

GetAvgRating(ratingArr) {
  if (ratingArr?.length === 0) return 0
  let totalReviewCount = 0;
  ratingArr?.forEach(element => {
    totalReviewCount += element.rating
  });

  const multiplier = Math.pow(10, 1)
  const avgReviewCount =
    Math.round((totalReviewCount / ratingArr?.length) * multiplier) / multiplier

  return avgReviewCount
}
}
