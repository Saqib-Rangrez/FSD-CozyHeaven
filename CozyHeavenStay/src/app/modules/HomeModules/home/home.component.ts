import { Component } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent {
  selectedLocation:any;
  selectedDateRange:"19 Sep to 28 Sep";
  guestsRooms:"2 Guests 1 Room";
  
  offers: any[]= [
    { imagepath:"./assets/HomeImages/offers/01.jpg", title:"Daily 50 Lucky Winners get a Free Stay", des:"Valid till: 15 Nov", offerpage:"offer-detail.html"},
    { imagepath:"./assets/HomeImages/offers/04.jpg", title:"Up to 60% OFF", des:"On Hotel Bookings Online", offerpage:"offer-detail.html"},
    { imagepath:"./assets/HomeImages/offers/03.jpg", title:"Book & Enjoy", des:"20% Off on the best available room rate", offerpage:"offer-detail.html"},
    { imagepath:"./assets/HomeImages/offers/02.jpg", title:"Hot Summer Nights", des:"Up to 3 nights free!", offerpage:"offer-detail.html"},
  ]

  hotels:any [] = [
    {Image: "./assets/HomeImages/hotel/01.jpg", location:"New York", title:"Baga Comfort", detailpage:"hotel-detail.html", price:"$455", rating:"4.5" },
    {Image: "./assets/HomeImages/hotel/02.jpg", location:"California", title:"New Apollo Hotel", detailpage:"hotel-detail.html", price:"$585", rating:"4.8" },
    {Image: "./assets/HomeImages/hotel/03.jpg", location:"Los Angeles", title:"New Age Hotel", detailpage:"hotel-detail.html", price:"$385", rating:"4.6" },
    {Image: "./assets/HomeImages/hotel/04.jpg", location:"Chicago", title:"Helios Beach Resort", detailpage:"hotel-detail.html", price:"$655", rating:"4.8" },
    
  ]

  clientImagepaths = [
    "./assets/HomeImages/client/01.svg",
    "./assets/HomeImages/client/02.svg",
    "./assets/HomeImages/client/03.svg",
    "./assets/HomeImages/client/04.svg",
    "./assets/HomeImages/client/05.svg",
    "./assets/HomeImages/client/06.svg",
  ]

  teams = [
    {svgpath:"./assets/HomeImages/team/02.svg", imagepath:"./assets/HomeImages/team/01.jpg", content:"Moonlight newspaper up its enjoyment agreeable depending. Timed voice share led him to widen noisy young. At weddings believed in laughing", title:"Billy Vasquez", subtitle:"Ceo of Apple"},
    
    {svgpath:"./assets/HomeImages/team/03.svg", imagepath:"./assets/HomeImages/team/02.jpg", content:"Passage its ten led hearted removal cordial. Preference any astonished unreserved Mrs. understood the Preference unreserved.", title:"Carolyn Ortiz", subtitle:"Ceo of Google"},
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


}
