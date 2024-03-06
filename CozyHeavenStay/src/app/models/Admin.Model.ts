export class Admin {
    adminId: number;
    firstName: string;
    lastName: string;
    email: string;
    password: string;
    profileImage?: string;
    role?: string;
    token?: string;
    resetPasswordExpires?: Date | null;
    expiresIn : Date
  
    constructor(adminId: number, firstName: string, lastName: string, email: string, password: string, profileImage?: string, role?: string, token?: string,resetPasswordExpires?: Date, expiresIn?: Date) {
      this.adminId = adminId;
      this.firstName = firstName;
      this.lastName = lastName;
      this.email = email;
      this.password = password;
      this.profileImage = profileImage;
      this.role = role;
      this.token = token;
    }
  }
  