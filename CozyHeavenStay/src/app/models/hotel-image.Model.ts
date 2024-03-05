import { Hotel } from "./hotel.Model";

export class HotelImage {
    imageId: number;
    hotelId: number;
    imageUrl: string;
    hotel: Hotel | null;

    constructor(imageId: number, hotelId: number, imageUrl: string, hotel: Hotel | null) {
        this.imageId = imageId;
        this.hotelId = hotelId;
        this.imageUrl = imageUrl;
        this.hotel = hotel;
    }
}