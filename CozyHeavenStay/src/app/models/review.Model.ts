import { Hotel } from "./hotel.Model";
import { User } from "./User.Model";

export class Review {
    reviewId: number;
    userId: number;
    hotelId: number;
    rating: number;
    comments: string;
    hotel?: Hotel; 
    user?: User;   

    constructor(
      reviewId: number,
      userId: number,
      hotelId: number,
      rating: number,
      comments: string,
      hotel?: Hotel,
      user?: User
    ) {
      this.reviewId = reviewId;
      this.userId = userId;
      this.hotelId = hotelId;
      this.rating = rating;
      this.comments = comments;
      this.hotel = hotel;
      this.user = user;
    }
  }
  