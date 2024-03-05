import { Room } from './room.Model';
import { HotelOwner } from './hotel-owner.Model';
import { HotelImage } from './hotel-image.Model';
import { Review } from './review.Model';
import { Booking } from './booking.Model';

export class Hotel {
  hotelId: number;
  ownerId?: number;
  name: string;
  location: string;
  description: string;
  amenities: string;
  owner?: HotelOwner;
  bookings?: Booking[];
  hotelImages?: HotelImage[];
  reviews?: Review[];
  rooms?: Room[];

  constructor(
    hotelId: number,
    name: string,
    location: string,
    description: string,
    amenities: string,
    ownerId?: number,
    owner?: HotelOwner,
    bookings?: Booking[],
    hotelImages?: HotelImage[],
    reviews?: Review[],
    rooms?: Room[]
  ) {
    this.hotelId = hotelId;
    this.ownerId = ownerId;
    this.name = name;
    this.location = location;
    this.description = description;
    this.amenities = amenities;
    this.owner = owner;
    this.bookings = bookings;
    this.hotelImages = hotelImages;
    this.reviews = reviews;
    this.rooms = rooms;
  }
}
