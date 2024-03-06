export class Booking {
    location: string | null;
    checkInDate: Date | null;
    checkOutDate: Date | null;
    numberOfRooms: number | null;
    numberOfAdults: number | null;
    numberOfChildren: number | null;
  
    constructor(
      location: string | null,
      checkInDate: Date | null,
      checkOutDate: Date | null,
      numberOfRooms: number | null,
      numberOfAdults: number | null,
      numberOfChildren: number | null
    ) {
      this.location = location;
      this.checkInDate = checkInDate;
      this.checkOutDate = checkOutDate;
      this.numberOfRooms = numberOfRooms;
      this.numberOfAdults = numberOfAdults;
      this.numberOfChildren = numberOfChildren;
    }
  }
  