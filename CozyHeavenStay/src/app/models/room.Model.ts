import { Hotel } from './hotel.Model';
import { Booking } from './booking.Model';
import { RoomImage } from './room-image.Model';

export class Room {
  roomId: number;
  hotelId?: number;
  roomType: string;
  maxOccupancy: number;
  bedType: string;
  baseFare: number;
  roomSize: string;
  acStatus: string;
  hotel?: Hotel;
  bookings?: Booking[];
  roomImages?: RoomImage[];

  constructor(
    roomId: number,
    roomType: string,
    maxOccupancy: number,
    bedType: string,
    baseFare: number,
    roomSize: string,
    acStatus: string,
    hotelId?: number,
    hotel?: Hotel,
    bookings?: Booking[],
    roomImages?: RoomImage[]
  ) {
    this.roomId = roomId;
    this.hotelId = hotelId;
    this.roomType = roomType;
    this.maxOccupancy = maxOccupancy;
    this.bedType = bedType;
    this.baseFare = baseFare;
    this.roomSize = roomSize;
    this.acStatus = acStatus;
    this.hotel = hotel;
    this.bookings = bookings;
    this.roomImages = roomImages;
  }
}
