import { Component, inject } from '@angular/core';
import { RegisterUserDTO } from '../../../models/DTO/RegisterUserDTO';
import {  FormControl, FormGroup, Validators } from '@angular/forms';
import { AuthAPIService } from '../../../services/operations/auth-api.service';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';

@Component({
  selector: 'app-signup-user',
  templateUrl: './signup-user.component.html',
  styleUrl: './signup-user.component.css'
})
export class SignupUserComponent {
  registerForm: FormGroup;
  router : Router = inject(Router);
  toastr : ToastrService = inject(ToastrService);
  authService : AuthAPIService = inject(AuthAPIService);
  showConfirmPassword: boolean = false;
  showPassword: boolean = false;


  ngOnInit() {
    this.registerForm = new FormGroup({
      firstName: new FormControl(null, [Validators.required]),
      lastName: new FormControl(null, [Validators.required]),
      email: new FormControl(null, [Validators.required, Validators.email]),
      password: new FormControl(null,  [Validators.required]),
      confirmPassword: new FormControl(null,  [Validators.required, this.passwordMatchValidator]),
      gender: new FormControl(null,  [Validators.required]),
      accountType : new FormControl(null, [Validators.required]),
      contactNumber: new FormControl(null,  [Validators.required]),
      address: new FormControl(null,  [Validators.required]),
    });
  }

  onSubmit() {
    if (this.registerForm.valid) {
      const formData: RegisterUserDTO = this.registerForm.value;

      console.log(formData);

      if(this.registerForm.get('accountType').value === 'User') {
        this.authService.signupUser(formData).subscribe({
          next : (res) => {
              console.log(res);
              this.toastr.success("Registration success")
              this.router.navigate(['/login'])
          },
          error : (err) => {
            this.toastr.error("Registration failed")
            console.log(err);
          }
        })
      }else{
        this.authService.signupOwner(formData).subscribe({
          next : (res) => {
            console.log(res);
            this.toastr.success("Registration success")
            this.router.navigate(['/login'])

          },
          error : (err) => {
            console.log(err);
            this.toastr.error("Registration failed")
          }
        })
      }           
    } else {
      console.log('Form is invalid');
      this.toastr.error("Please provide a valid registration details");
    }
  }

  toggleShowConfirmPassword() {
    this.showConfirmPassword = !this.showConfirmPassword;
  }
  toggleShowPassword() {
    this.showPassword = !this.showPassword;
  }

  passwordMatchValidator() {
    const password = this?.registerForm.get('password')?.value;
    const confirmPassword = this?.registerForm.get('confirmPassword')?.value;
    return password === confirmPassword ? null : { passwordMismatch: true };
  }
}
