export class HotelDTO {
  
    Name: string;
    ownerId: number;
    Amenities: string;
    Description: string;
    Location: string;
    Files: File[];

  constructor(
    name: string,
    location: string,
    description: string,
    amenities: string,
    ownerId: number,
    files: File[]
  ) {
    this.ownerId = ownerId;
    this.Name = name;
    this.Location = location;
    this.Description = description;
    this.Amenities = amenities;
    this.Files = files;
  }
  
}
