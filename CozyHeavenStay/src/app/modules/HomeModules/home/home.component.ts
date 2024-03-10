import { Component } from '@angular/core';
import { HotelService } from '../../../services/hotel.service';
import { toArray } from 'rxjs';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent{
  user;
  constructor(private hotelService: HotelService){}

  hotelList;
  
  offers: any[]= [
    { imagepath:"./assets/HomeImages/offers/01.jpg", title:"Daily 50 Lucky Winners get a Free Stay", des:"Valid till: 15 Nov", offerpage:"offer-detail.html"},
    { imagepath:"./assets/HomeImages/offers/04.jpg", title:"Up to 60% OFF", des:"On Hotel Bookings Online", offerpage:"offer-detail.html"},
    { imagepath:"./assets/HomeImages/offers/03.jpg", title:"Book & Enjoy", des:"20% Off on the best available room rate", offerpage:"offer-detail.html"},
    { imagepath:"./assets/HomeImages/offers/02.jpg", title:"Hot Summer Nights", des:"Up to 3 nights free!", offerpage:"offer-detail.html"},
  ]
  

  hotels:any [] = []
  reviews;

  clientImagepaths = [
    "./assets/HomeImages/client/01.svg",
    "./assets/HomeImages/client/02.svg",
    "./assets/HomeImages/client/03.svg",
    "./assets/HomeImages/client/04.svg",
    "./assets/HomeImages/client/05.svg",
    "./assets/HomeImages/client/06.svg",
  ]

  
  nearbycards = [
    {image:"./assets/HomeImages/hotel/nearby/01.jpg", location:"San Francisco", time:"13 min drive"},
    {image:"./assets/HomeImages/hotel/nearby/02.jpg", location:"Los Angeles", time:"25 min drive"},
    {image:"./assets/HomeImages/hotel/nearby/03.jpg", location:"Miami", time:"45 min drive"},
    {image:"./assets/HomeImages/hotel/nearby/04.jpg", location:"Sanjosh", time:"55 min drive"},
    {image:"./assets/HomeImages/hotel/nearby/05.jpg", location:"New York", time:"1 hour drive"},
    {image:"./assets/HomeImages/hotel/nearby/06.jpg", location:"North Justen", time:"2 hour drive"},
    {image:"./assets/HomeImages/hotel/nearby/07.jpg", location:"Rio", time:"20 min drive"},
    {image:"./assets/HomeImages/hotel/nearby/08.jpg", location:"Las Vegas", time:"3 hour drive"},
    {image:"./assets/HomeImages/hotel/nearby/09.jpg", location:"Texas", time:"55 min drive"},
    {image:"./assets/HomeImages/hotel/nearby/10.jpg", location:"Chicago", time:"13 min drive"},
    {image:"./assets/HomeImages/hotel/nearby/11.jpg", location:"New Keagan", time:"35 min drive"},
    {image:"./assets/HomeImages/hotel/nearby/01.jpg", location:"Oslo", time:"1 hour 13 min drive"}

  ]


  ngOnInit():void{
    this.user = JSON.parse(localStorage.getItem('user'));
    this.hotelService.getAllHotels('').subscribe({next:(res) => {
      this.hotelList = res;
      console.log(this.hotelList);
      console.log("api response: " + res);
      this.reviews = this.hotelList.data[0].reviews;
      
      for (let index = 0; index < Math.min(4, this.hotelList.data.length); index++) {
        const h = this.hotelList.data[index];
        console.log("Processing hotel:", h);
        const avgrating = this.GetAvgRating(h.reviews);
        const minPrice = this.GetMinPrice(h.rooms);
        

        this.hotels.push({ Image: h.hotelImages[0].imageUrl, location: h.location, title: h.name, detailpage: "/hoteldetail/"+h.hotelId, price: "Rs"+minPrice, rating: avgrating });
      }
      
    },
    error:error => {
      console.log(error);
    }
  } 
  )

  
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
}

