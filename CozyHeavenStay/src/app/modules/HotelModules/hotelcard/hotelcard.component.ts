import { Component, Input, AfterViewInit, ElementRef, ViewChild, inject } from '@angular/core';
import { NavigationEnd, Router } from '@angular/router';
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
router : Router = inject(Router);

ngOnInit() {
  this.avgRating = this.GetAvgRating(this.card.reviews);

  this.router.events.subscribe((event) => {
    if (event instanceof NavigationEnd) {
      window.scrollTo(0, 0);
    }
  });
}

GetMinPrice(rooms){
  const minPriceRoom = rooms.reduce((minRoom, currentRoom) => {
    // If minRoom is null or the baseFare of currentRoom is less than minRoom's baseFare
    if (!minRoom || currentRoom?.baseFare < minRoom?.baseFare) {
      return currentRoom;
    }
    return minRoom;
  }, null);
  
  // Now minPriceRoom will contain the room with the minimum price
  return minPriceRoom?.baseFare;
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
