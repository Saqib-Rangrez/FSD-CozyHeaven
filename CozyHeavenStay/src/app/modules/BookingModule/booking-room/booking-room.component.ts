import { Component, inject } from '@angular/core';
import { RoomService } from '../../../services/room.service';
import { HotelService } from '../../../services/hotel.service';
import { ActivatedRoute, NavigationEnd, Router } from '@angular/router';
import { Room } from '../../../models/room.Model';
import { User } from '../../../models/User.Model';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { BookingService } from '../../../services/booking.service';
import { Booking } from '../../../models/booking.Model';
import { Payment } from '../../../models/payment.Model';
import { PaymentService } from '../../../services/payment.service';
import { ToastrService } from 'ngx-toastr';
import { ReviewService } from '../../../services/review.service';
import { RazorpayService } from '../../../services/razorpay.service';


@Component({
  selector: 'app-booking-room',
  templateUrl: './booking-room.component.html',
  styleUrl: './booking-room.component.css'
})
export class BookingRoomComponent {
  toastr : ToastrService = inject(ToastrService); 
  roomId: number;
  room: Room;
  response;
  user: User;
  avgRating:number = 0;
  selectedDates: Date[];
  minDate: Date; 
  numberOfRooms: number = 1;
  numberOfAdults: number = 1;
  numberOfChildren: number = 0;
  guestForm: FormGroup;
  guests: FormGroup[];
  priceData: any = {
    roomCharges:0,
    discountPercentage: 40, // Example discount percentage
    discountAmount: 0, // Example discount amount
    taxesAndFees: 100, // Example taxes and fees
    priceAfterDiscount:0,
    additionalGuestsCharges:0,
    totalAmount:0
  };
  bookingList = [];
  booking: Booking;
  createdBooking: Booking;
  payment: Payment;
  paymentCreated: Payment;
  reviewService : ReviewService = inject(ReviewService);

  constructor(private roomService: RoomService,private hotelService: HotelService, 
    private activatedRoute: ActivatedRoute, private fb: FormBuilder,
    private bookingService:BookingService, private paymentService: PaymentService,
    private router: Router, private razorpayService: RazorpayService
    ){
    //Set minimum selectable date to today
    this.minDate = new Date();
   
    
    const today = new Date();
    const tomorrow = new Date();
    tomorrow.setDate(tomorrow.getDate() + 1);
    this.selectedDates = [today, tomorrow];
    
    this.guests = [];
  }
  
  onActivate(_event: any) {
    window.scroll({
        top: 0,
        left: 0,
        behavior: 'smooth'
    });
}

  ngOnInit():void{
    

    this.roomId = this.activatedRoute.snapshot.params['roomid'];
    
    this.user = JSON.parse(localStorage.getItem('user'));
    this.createGuestForm();

    this.bookingService.getAllBookings(this.user.token).subscribe({
      next : (res) => {
        this.response = res;
        this.bookingList = this.response.data;
        this.bookingList = this.bookingList?.filter(booking => booking?.roomId == this.roomId);
      },
      error : (err) => {
        console.log(err);
      }
    })

    
    this.roomService.getRoomById(this.roomId,this.user.token).subscribe({
      next : (res) => {
        this.response = res;
        this.room = this.response.data;

        this.reviewService.getReviewByHotelId(this?.room?.hotelId,this.user.token).subscribe({
          next : (res) => {
            this.avgRating = this.GetAvgRating(res.data);
          },
          error : (err) => {

            console.log(err);
          }
        })        
      },
      error : (err) => {
        console.log(err);
      }
    })    

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


  addAdult(data){
    if(data) {
       this.numberOfAdults = this.numberOfAdults + 1;

    }
    else{
      if(this.numberOfAdults > 1) {
        
        this.numberOfAdults = this.numberOfAdults - 1;
      }      
    }    
  }

  noOfChildren(data) {
    if(data) {
      
        this.numberOfChildren = this.numberOfChildren + 1;
      
    }else{
      if(this.numberOfChildren > 0){
        
        this.numberOfChildren = this.numberOfChildren - 1;
       
      }
      
    }    
  }

  noOfRooms(data) {
    if(data) {
      if(this.numberOfRooms <  10){
        
        this.numberOfRooms = this.numberOfRooms + 1;
        
      }      
    }else{
      if(this.numberOfRooms > 1){
        
          this.numberOfRooms = +this.numberOfRooms - 1;
        
      }
      
    }    
  }

  calculateDuration(checkin: Date, checkout: Date) {
    // Check-in and check-out times
    const checkinTime = { hours: 11, minutes: 30 };
    const checkoutTime = { hours: 18, minutes: 30 };

    // Adjust check-in and check-out dates based on times
    checkin.setHours(checkinTime.hours, checkinTime.minutes, 0, 0);
    checkout.setHours(checkoutTime.hours, checkoutTime.minutes, 0, 0);

    // Calculate the difference in milliseconds
    const differenceMs = checkout.getTime() - checkin.getTime();

    // Convert difference to days and nights
    const differenceDays = Math.ceil(differenceMs / (1000 * 60 * 60 * 24));
    const differenceNights = differenceDays - 1;

    // return duration 
    return `${differenceNights} Nights - ${differenceDays} Days`;
  }


  createGuestForm(): void {
    this.guestForm = this.fb.group({
      title: [(this.user?.gender?.toLowerCase() === 'male') ? 'Mr' : 'Mrs', Validators.required],
      firstName: [this.user?.firstName, Validators.required],
      lastName: [this.user?.lastName, Validators.required]
    });
  }

  addNewGuest(): void {
    this.guests.push(this.fb.group({
      title: ['', Validators.required],
      firstName: ['', Validators.required],
      lastName: ['', Validators.required]
    }));
  }

  removeGuest(index: number): void {
    this.guests.splice(index, 1);
  }

  saveGuests(): void {
    // Save guest data
    console.log(this.guests);
  }

  calculateAdditionalCharges(): number {
    let additionalChargePercentage = 20;
    let additionalCharges = 0;
    let additionalGuests = 0;
    let totalGuest = this.numberOfAdults + this.numberOfChildren;
    let totalOccupancy = this.numberOfRooms * this.room?.maxOccupancy;
    
    if(totalGuest > totalOccupancy){
      additionalGuests = totalGuest - totalOccupancy;
      
      if(this.numberOfChildren > 0){
        if(additionalGuests < this.numberOfChildren){
          additionalCharges += additionalGuests * ( (this.room?.baseFare * additionalChargePercentage) / 100);
          additionalGuests = 0;
        }
        else{
          additionalCharges += this.numberOfChildren * ( (this.room?.baseFare * additionalChargePercentage) / 100);
          additionalGuests = additionalGuests - this.numberOfChildren;
        }
      }
      
      if(additionalGuests > 0){
        additionalChargePercentage = 40;

        additionalCharges += additionalGuests * ( (this.room?.baseFare * additionalChargePercentage) / 100);
        
      }
      
    }
    
    this.priceData.additionalGuestsCharges = additionalCharges;
    
    return this.priceData.additionalGuestsCharges;
  }

  calculateRoomCharges(): number {
    const inputString = (this.calculateDuration(this.selectedDates[0],this.selectedDates[1]));
    const parts = inputString.split(' ');
  
  const nightsStr = parts[0];
  const daysStr = parts[3]; 

  const nights = parseInt(nightsStr, 10); 
  const days = parseInt(daysStr, 10);

  const maxNightOrDay = Math.max(nights, days);
   
  this.priceData.roomCharges = maxNightOrDay * (this.numberOfRooms * this.room?.baseFare);
    
    return this.priceData.roomCharges;
  }

  calculateDiscountAmount(): number {
    this.priceData.discountAmount = (this.calculateRoomCharges() * this.priceData.discountPercentage) / 100;
    return this.priceData.discountAmount;
  }

  calculatePriceAfterDiscount(): number {
    this.priceData.priceAfterDiscount = this.calculateRoomCharges() - this.calculateDiscountAmount();
    return this.priceData.priceAfterDiscount;
  }

  calculatePayableNow(): number {
    this.priceData.totalAmount = this.calculatePriceAfterDiscount() + this.priceData.taxesAndFees + this.calculateAdditionalCharges();
    return this.priceData.totalAmount;
  }

  //popup
  
  createPay(paymentMode:string, status: string){
    this.payment = new Payment(
      0,
      this.createdBooking.bookingId,
      paymentMode,
      status,
      this.priceData.totalAmount,
      new Date()
    );
    
    this.paymentService.createPayment(this.payment,this.user.token).subscribe(
      {
      next: (res) => {
        this.response = res;
        this.paymentCreated = this.response.data;
        
        this.updateBookingPayment(this.paymentCreated.paymentId, status);       
      },
      error: (err) => {
        console.error(err);
      }
    });
  }


  openModel() {

   this.booking = new Booking(
      0,
      this.numberOfAdults+this.numberOfChildren,
      this.selectedDates[0],
      this.selectedDates[1],
      this.priceData.totalAmount,
      'Pending',
      this.user.userId,
      this.room.roomId,
      this.room.hotelId,
      null
    );

    this.bookingService.createBooking(this.booking,this.user.token).subscribe({
      next: (res) => {
        this.response = res;
        this.createdBooking = this.response.data;
        
        this.toastr.success("Booking Success !!")
      },
      error: (err) => {
        console.error(err);
        this.toastr.error("Booking Failed !!")
      }
    });

    const modelDiv = document.getElementById('myModal');
    if(modelDiv!= null) {
      modelDiv.style.display = 'block';
    } 
  }

  updateBookingPayment(paymentId: number, status : string) {
    if (!this.createdBooking) {
      console.error('No booking data available.');
      return;
    }

    this.createdBooking.paymentId = paymentId;
    if(status != 'Pending')
    this.createdBooking.status = 'Confirmed';
    

    // Call the BookingService to update the booking
    this.bookingService.updateBooking(this.createdBooking,this.user.token).subscribe(
      {
        next: (res) => {
          this.response = res;        
        },
        error: (err) => {
          console.error(err);
        }
      });
  }

  CloseModel() {
    const modelDiv = document.getElementById('myModal');
    if(modelDiv!= null) {
      modelDiv.style.display = 'none';
    } 
  }

  PayNow(){
    //RazorPay Start
    const obj = {amount : 1};
    this.razorpayService.createOrder(obj).subscribe(
      {
        next: (res) => {
          this.response = res;  
          console.log(this.response);
          this.openRazorpayCheckout(res);
          
        },
        error: (err) => {
          console.error(err);
        },
        
      });
    //Razorpay END

    this.createPay("Online","Paid");    
    this.CloseModel();
  }

  verifyPayment(paymentDetails: any) {
    this.razorpayService.verifyPayment(paymentDetails).subscribe(
      (response) => {
        console.log('Payment verified successfully:', response);  
        this.router.navigate(['/confirm', this.createdBooking.bookingId]);     
        this.toastr.success("Payment Success !!") 
      },
      (error) => {
        console.error('Error verifying payment:', error);
      },
   
    );
  }

  openRazorpayCheckout(order: any) {
    const options = {
      key: order.key,
      amount: order.amount * 100, 
      currency: order.currency,
      name: order.name,
      order_id: order.order_id,
      handler: (response) => {
        console.log(response);
        
        this.verifyPayment(response);
      },
      prefill: {
        name: 'CozyHeaven Stay',
        email: 'cozyheavenstay@gmail.com',
        contact: '+91-9900776669'
      },
      notes: {
        address: 'Razorpay Corporate Office'
      },
      theme: {
        color: '#3399cc'
      },
      callback_url: order.callback_url 
    };
    const rzp1 = new Razorpay(options);
    rzp1.open();
   }

  

  PayLater(){
    this.createPay("None", "Pending");
    
    this.toastr.success("Payment Pending!!")
    this.CloseModel();
    this.router.navigate(['/dashboard/bookings']);  
  }

    isRoomAvailable(checkInDate: Date, checkOutDate: Date): boolean {
      for (const booking of this?.bookingList) {
          const bookingCheckInDate = new Date(booking.checkInDate);
          const bookingCheckOutDate = new Date(booking.checkOutDate);

          if (checkInDate >= bookingCheckInDate && checkInDate < bookingCheckOutDate) {
              return false; 
          }

          if (checkOutDate > bookingCheckInDate && checkOutDate <= bookingCheckOutDate) {
              return false; 
          }
      }
      return true;
    }


    BookNow(){
      if(this.isRoomAvailable(this.selectedDates[0],this.selectedDates[1])){
        this.openModel();
      }
      else{
        this.toastr.error("Not Available on you Dates!! Please check dates!!!!");
      }
    }
}
