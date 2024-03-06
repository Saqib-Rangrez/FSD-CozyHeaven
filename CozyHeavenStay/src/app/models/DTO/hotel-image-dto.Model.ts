export class HotelImageDTO {
    hotelId: number;
    imageUrl: string;

    constructor(hotelId: number, imageUrl: string) {
        this.hotelId = hotelId;
        this.imageUrl = imageUrl;
    }
}