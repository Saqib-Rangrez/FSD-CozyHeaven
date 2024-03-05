export class User {
    userId: number;
    firstName: string;
    lastName: string;
    email: string;
    password: string;
    gender: string;
    contactNumber: string;
    address: string;
    role?: string | null;
    profileImage?: string | null;
    token?: string | null;
    resetPasswordExpires?: Date | null;
    expiresIn : Date
}
  