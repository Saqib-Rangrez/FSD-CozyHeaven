export class ResetPasswordDTO {
    email: string;
    password: string;
    confirmPassword: string;
    token: string;
  
    constructor(email: string, password: string, confirmPassword: string, token: string) {
      this.email = email;
      this.password = password;
      this.confirmPassword = confirmPassword;
      this.token = token;
    }
  }