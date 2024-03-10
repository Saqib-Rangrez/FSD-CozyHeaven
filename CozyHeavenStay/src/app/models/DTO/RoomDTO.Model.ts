export class RoomDTO {
    hotelId: number | null;
    roomType: string;
    maxOccupancy: number;
    bedType: string;
    baseFare: number;
    roomSize: string;
    acStatus: string;
    files: File[] | null;
  
    constructor(
      hotelId: number | null,
      roomType: string,
      maxOccupancy: number,
      bedType: string,
      baseFare: number,
      roomSize: string,
      acStatus: string,
      files: File[] | null
    ) {
      this.hotelId = hotelId;
      this.roomType = roomType;
      this.maxOccupancy = maxOccupancy;
      this.bedType = bedType;
      this.baseFare = baseFare;
      this.roomSize = roomSize;
      this.acStatus = acStatus;
      this.files = files;
    }
  }
  