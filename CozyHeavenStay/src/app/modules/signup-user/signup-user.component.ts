import { Component, inject } from '@angular/core';
import { RegisterUserDTO } from '../../models/DTO/RegisterUserDTO';
import {  FormControl, FormGroup, Validators } from '@angular/forms';
import { AuthAPIService } from '../../services/operations/auth-api.service';

@Component({
  selector: 'app-signup-user',
  templateUrl: './signup-user.component.html',
  styleUrl: './signup-user.component.css'
})
export class SignupUserComponent {
  registerForm: FormGroup;
  authService : AuthAPIService = inject(AuthAPIService);


  ngOnInit() {
    this.registerForm = new FormGroup({
      firstName: new FormControl(null, [Validators.required]),
      lastName: new FormControl(null, [Validators.required]),
      email: new FormControl(null, [Validators.required, Validators.email]),
      password: new FormControl(null,  [Validators.required]),
      confirmPassword: new FormControl(null,  [Validators.required, this.passwordMatchValidator]),
      gender: new FormControl(null,  [Validators.required]),
      contactNumber: new FormControl(null,  [Validators.required]),
      address: new FormControl(null,  [Validators.required]),
    });
  }

  onSubmit() {
    if (this.registerForm.valid) {
      const formData: RegisterUserDTO = this.registerForm.value;
      // Do something with formData, like sending it to the server
      this.authService.signupOwner(formData).subscribe({
        next : (res) => {
          console.log(res);
        },
        error : (err) => {
          console.log(err);
        }
      })
      console.log(formData);
    } else {
      console.log('Form is invalid');
    }
  }

  passwordMatchValidator(formGroup: FormGroup) {
    const password = formGroup.get('password')?.value;
    const confirmPassword = formGroup.get('confirmPassword')?.value;
    return password === confirmPassword ? null : { passwordMismatch: true };
  }
}
