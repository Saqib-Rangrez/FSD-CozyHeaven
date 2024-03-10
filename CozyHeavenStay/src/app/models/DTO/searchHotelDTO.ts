export class Booking {
    Location: string | null;
    CheckInDate: Date | null;
    CheckOutDate: Date | null;
    NumberOfRooms: number | null;
    NumberOfAdults: number | null;
    NumberOfChildren: number | null;
  
    constructor(
      Location: string | null,
      CheckInDate: Date | null,
      CheckOutDate: Date | null,
      NumberOfRooms: number | null,
      NumberOfAdults: number | null,
      NumberOfChildren: number | null
    ) {
      this.Location = Location;
      this.CheckInDate = CheckInDate;
      this.CheckOutDate = CheckOutDate;
      this.NumberOfRooms = NumberOfRooms;
      this.NumberOfAdults = NumberOfAdults;
      this.NumberOfChildren = NumberOfChildren;
    }
  }
  