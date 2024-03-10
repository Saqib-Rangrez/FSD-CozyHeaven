import { Component } from '@angular/core';
import { HotelService } from '../../../services/hotel.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Hotel } from '../../../models/hotel.Model';

@Component({
  selector: 'app-hotel-detail',
  templateUrl: './hotel-detail.component.html',
  styleUrl: './hotel-detail.component.css'
})
export class HotelDetailComponent {
  hoteId;
  hotelname;
  response;
  user;
  minPriceRoom:number = 0;
  hotel:Hotel;
  avgRating : number = 0;
  reviewRatings:number[];
  percentageForEachRating=[];

  constructor(private hotelService:HotelService, private activatedRoute:ActivatedRoute, private router: Router){}

  ngOnInit():void{
    this.user = JSON.parse(localStorage.getItem('user'));
    this.hoteId = this.activatedRoute.snapshot.params['id'];
    console.log(this.hoteId);

    this.hotelService.getHotelById(this.hoteId,this.user.token).subscribe({
      next : (res) => {
      
        this.response = res;
        this.hotel = this.response.data;
        this.avgRating = this.GetAvgRating(this.hotel.reviews);
        console.log(this.hotel);
        this.hotelname = this.hotel.name;
       
        this.minPriceRoom = this.GetMinPrice(this.hotel.rooms);
       
        this.reviewRatings = this.extractRatings(this.hotel.reviews);
        
        this.percentageForEachRating = this.calculatePercentageForEachRating(this.reviewRatings);
        
        
      },
      error : (err) => {
        console.log(err);
      }
    }) 

    

  }

  extractRatings(reviews: any[]): number[] {
    return reviews.map(review => review.rating);
  }

  countStar(star: number): number {
    return this.reviewRatings.filter(rating => rating === star).length;
  }

  calculatePercentageForEachRating(ratings: number[]): { rating: number, percentage: number }[] {
    const totalReviews = ratings.length;

    return [1,2,3,4,5].map((rating, index) => ({
      rating: rating,
      percentage: (this.countStar(rating) / totalReviews) * 100
    }));
  }

  GetMinPrice(rooms){
    const minPriceRoom = rooms.reduce((minRoom, currentRoom) => {
      // If minRoom is null or the baseFare of currentRoom is less than minRoom's baseFare
      if (!minRoom || currentRoom.baseFare < minRoom.baseFare) {
        return currentRoom;
      }
      return minRoom;
    }, null);
    
    // Now minPriceRoom will contain the room with the minimum price
    return minPriceRoom.baseFare;
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

  navigateToRoomOptions() {
    this.router.navigateByUrl("#room-options"); 
}

}
