import { Room } from './room.Model';
import { Hotel } from './hotel.Model';
import { User } from './User.Model';
import { Payment } from './payment.Model';

export class Booking {
  bookingId: number;
  userId?: number;
  roomId?: number;
  hotelId?: number;
  paymentId?: number;
  numberOfGuests: number;
  checkInDate: Date;
  checkOutDate: Date;
  totalFare: number;
  status: string;
  room?: Room;
  hotel?: Hotel;
  user?: User;
  payment?: Payment;

  constructor(
    bookingId: number,
    numberOfGuests: number,
    checkInDate: Date,
    checkOutDate: Date,
    totalFare: number,
    status: string,
    userId?: number,
    roomId?: number,
    hotelId?: number,
    paymentId?: number,
    room?: Room,
    hotel?: Hotel,
    user?: User,
    payment?: Payment
  ) {
    this.bookingId = bookingId;
    this.userId = userId;
    this.roomId = roomId;
    this.hotelId = hotelId;
    this.paymentId = paymentId;
    this.numberOfGuests = numberOfGuests;
    this.checkInDate = checkInDate;
    this.checkOutDate = checkOutDate;
    this.totalFare = totalFare;
    this.status = status;
    this.room = room;
    this.hotel = hotel;
    this.user = user;
    this.payment = payment;
  }
}
