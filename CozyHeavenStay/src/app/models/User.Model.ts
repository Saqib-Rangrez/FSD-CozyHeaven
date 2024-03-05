export class User {
    userId: number;
    firstName: string;
    lastName: string;
    email: string;
    password: string;
    gender: string;
    contactNumber: string;
    address: string;
    role?: string | "null";
    profileImage?: string | "null";
    token?: string | "null";
    resetPasswordExpires?: Date | "null";
    expiresIn: Date;
  
    constructor(
      userId: number,
      firstName: string,
      lastName: string,
      email: string,
      password: string,
      gender: string,
      contactNumber: string,
      address: string,
      expiresIn: Date,
      role?: string,
      profileImage?: string,
      token?: string,
      resetPasswordExpires?: Date,
    ) {
      this.userId = userId;
      this.firstName = firstName;
      this.lastName = lastName;
      this.email = email;
      this.password = password ;
      this.gender = gender;
      this.contactNumber = contactNumber;
      this.address = address;
      this.role = role;
      this.profileImage = profileImage;
      this.token = token;
      this.resetPasswordExpires = resetPasswordExpires;
      this.expiresIn = expiresIn ;
    }
  }
  