import { Component, inject } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { AuthAPIService } from '../../../services/operations/auth-api.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {
  loginForm: FormGroup;
  authService : AuthAPIService = inject(AuthAPIService);
  toastr : ToastrService = inject(ToastrService);
  showPassword: boolean = false;

  ngOnInit(): void {
    this.loginForm = new FormGroup({
      email: new FormControl(null, [Validators.required, Validators.email]),
      password: new FormControl (null, [Validators.required]),
      // accountType : new FormControl(null, [Validators.required])
    });
  }

  onSubmit() {
    if (this.loginForm.valid) {
      const email = this.loginForm.value.email;
      const password = this.loginForm.value.password;


      if(this.loginForm.get('accountType').value === 'Admin') {
        this.authService.loginAdmin(email, password).subscribe({
          next : (res) => {
            console.log(res);
            this.toastr.success("Login success")
          },
          error: (err) => {
            console.log(err);
            this.toastr.success("Login failed")
          }
        });
  
      }else if(this.loginForm.get('accountType').value === 'User') {
        this.authService.loginUser(email, password).subscribe({
          next : (res) => {
            console.log(res);
            this.toastr.success("Login success")
          },
          error: (err) => {
            console.log(err);
            this.toastr.success("Login failed")
          }
        });
  
      }else{
        this.authService.loginOwner(email, password).subscribe({
          next : (res) => {
            console.log(res);
            this.toastr.success("Login success");
          },
          error: (err) => {
            console.log(err);
            this.toastr.success("Login failed")
          }
        });
  
      }      

    } else {
      this.toastr.error("Please provide a valid registration details");
      console.log("Form validation failed!");  
    }
  }

  toggleShowPassword() {
    this.showPassword = !this.showPassword;
  }
}