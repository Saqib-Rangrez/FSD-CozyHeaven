import { Component, inject } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { AuthAPIService } from '../../../services/operations/auth-api.service';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {
  loginForm: FormGroup;
  authService : AuthAPIService = inject(AuthAPIService);
  toastr : ToastrService = inject(ToastrService);
  router : Router = inject(Router);
  showPassword: boolean = false;

  ngOnInit(): void {
    this.loginForm = new FormGroup({
      email: new FormControl(null, [Validators.required, Validators.email]),
      password: new FormControl (null, [Validators.required]),
      accountType : new FormControl("User", [Validators.required])
    });
  }

  onSubmit() {
    if (this.loginForm.valid) {
      const email = this.loginForm.value.email;
      const password = this.loginForm.value.password;


      if(this.loginForm.get('accountType').value === 'Admin') {
        this.authService.loginAdmin(email, password).subscribe({
          next : (res) => {
            this.toastr.success("Login success")
            this.router.navigate(['/home']);
          },
          error: (err) => {
            console.log(err);
            this.toastr.success("Invalid credentials")
          }
        });
  
      }else if(this.loginForm.get('accountType').value === 'User') {
        this.authService.loginUser(email, password).subscribe({
          next : (res) => {
            this.toastr.success("Login success");
            this.router.navigate(['/home']);
          },
          error: (err) => {
            console.log(err);
            this.toastr.success("Invalid credentials")
          }
        });
  
      }else{
        this.authService.loginOwner(email, password).subscribe({
          next : (res) => {
            this.toastr.success("Login success");
            this.router.navigate(['/home']);
          },
          error: (err) => {
            console.log(err);
            this.toastr.error("Invalid credentials")
          }
        });  
      }      
    } else {
      this.toastr.error("Please provide a valid registration details");
    }
  }

  toggleShowPassword() {
    this.showPassword = !this.showPassword;
  }
}