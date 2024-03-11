import { Component, inject } from '@angular/core';
import { HotelService } from '../../../services/hotel.service';
import { ActivatedRoute, NavigationEnd, Router } from '@angular/router';
import { Hotel } from '../../../models/hotel.Model';
import { ReviewService } from '../../../services/review.service';
import { Review } from '../../../models/review.Model';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-hotel-detail',
  templateUrl: './hotel-detail.component.html',
  styleUrl: './hotel-detail.component.css'
})
export class HotelDetailComponent {
  toastr : ToastrService = inject(ToastrService); 
  hotelId;
  hotelname;
  response;
  user;
  minPriceRoom:number = 0;
  hotel:Hotel;
  avgRating : number = 0;
  reviewRatings:number[];
  percentageForEachRating=[];
  reviewForm: FormGroup;
  reviewlength=5;

  constructor(private hotelService:HotelService, private activatedRoute:ActivatedRoute, 
    private router: Router, private reviewService:ReviewService, private formBuilder: FormBuilder){}

  ngOnInit():void{
    this.user = JSON.parse(localStorage.getItem('user'));
    this.hotelId = this.activatedRoute.snapshot.params['id'];
    console.log(this.hotelId);
    
    this.activatedRoute.fragment.subscribe((value) => {
          setTimeout(() => {
        const element = document.getElementById(value);
        if (element) {          
          element.scrollIntoView({ behavior: "smooth" });
        } else {
          console.error(`Element with ID '${value}' not found.`);
        }
      });
    });

    this.reviewForm = this.formBuilder.group({
      rating: [5, Validators.required],
      reviewMessage: ['', Validators.required]
    });

    this.hotelService.getHotelById(this.hotelId,this.user.token).subscribe({
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

    this.router.events.subscribe((event) => {
      if (event instanceof NavigationEnd) {
        window.scrollTo(0, 0);
      }
    });

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
      percentage: parseFloat(((this.countStar(rating) / totalReviews) * 100).toFixed(2))
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

submitReview() {
  if (this.reviewForm.valid) {
    const reviewData = new Review(
      0,
      this.user.userId,
      this.hotelId,
      this.reviewForm.value.rating,
      this.reviewForm.value.reviewMessage
    );

    this.reviewService.addReview(reviewData, this.user.token).subscribe(
      {
        next: (res) => {
          this.response = res;
          console.log('Review submitted successfully:', this.response.data);
          this.reviewForm.reset({ rating: 5, reviewMessage: '' });
          this.toastr.success("Review added Success !!")
          this.ngOnInit();
        },
        error: (err) => {
          console.error(err);
        }
      }
    );
  }
}


MoreReviews(){
  if(this.reviewlength < this.hotel.reviews.length){
    this.reviewlength += 5;
    console.log(this.reviewlength)
  }

}
}
