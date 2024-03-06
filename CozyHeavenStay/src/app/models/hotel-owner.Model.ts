import { Hotel } from "./hotel.Model";

export class HotelOwner {
    ownerId: number;
    firstName: string;
    lastName: string;
    email: string;
    password: string;
    gender: string;
    contactNumber: string;
    address: string;
    profileImage?: string;
    role?: string;
    token?: string;
    resetPasswordExpires?: Date;
    hotels?: Hotel[];
  
    constructor(
      ownerId: number,
      firstName: string,
      lastName: string,
      email: string,
      password: string,
      gender: string,
      contactNumber: string,
      address: string,
      profileImage?: string,
      role?: string,
      token?: string,
      resetPasswordExpires?: Date,
      hotels?: Hotel[]
    ) {
      this.ownerId = ownerId;
      this.firstName = firstName;
      this.lastName = lastName;
      this.email = email;
      this.password = password;
      this.gender = gender;
      this.contactNumber = contactNumber;
      this.address = address;
      this.profileImage = profileImage;
      this.role = role;
      this.token = token;
      this.resetPasswordExpires = resetPasswordExpires;
      this.hotels = hotels || [];
    }
  }
  