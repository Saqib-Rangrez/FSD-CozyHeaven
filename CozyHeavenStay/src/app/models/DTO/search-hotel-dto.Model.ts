export class SearchHotelDTO {
    location?: string;
    checkInDate?: Date;
    checkOutDate?: Date;
    numberOfRooms?: number;
    numberOfAdults?: number;
    numberOfChildren?: number;


    constructor(
        location?: string,
        checkInDate?: Date,
        checkOutDate?: Date,
        numberOfRooms?: number,
        numberOfAdults?: number,
        numberOfChildren?: number
    ) {
        this.location = location;
        this.checkInDate = checkInDate;
        this.checkOutDate = checkOutDate;
        this.numberOfRooms = numberOfRooms;
        this.numberOfAdults = numberOfAdults;
        this.numberOfChildren = numberOfChildren;
    }
  }
  
