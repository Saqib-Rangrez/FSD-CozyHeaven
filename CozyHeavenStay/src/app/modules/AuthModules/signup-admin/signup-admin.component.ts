import { Component, inject } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { RegisterAdminDTO } from '../../../models/DTO/RegisterAdminDTO';
import { AuthAPIService } from '../../../services/operations/auth-api.service';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';

@Component({
  selector: 'app-signup-admin',
  templateUrl: './signup-admin.component.html',
  styleUrl: './signup-admin.component.css'
})
export class SignupAdminComponent {
  adminForm: FormGroup;
  router : Router = inject(Router)
  toastr : ToastrService = inject(ToastrService);
  authService : AuthAPIService = inject(AuthAPIService);
  showConfirmPassword: boolean = false;
  showPassword: boolean = false;


  ngOnInit(): void {
    this.adminForm = new FormGroup({
      firstName: new FormControl(null, [ Validators.required]),
      lastName: new FormControl(null, [ Validators.required]),
      email: new FormControl(null, [ Validators.required, Validators.email]),
      password: new FormControl(null, [ Validators.required]),
      confirmPassword: new FormControl(null, [ Validators.required])
    });
  }

  onSubmit() {
    if (this.adminForm.valid) {
      const formData: RegisterAdminDTO = this.adminForm.value;

      this.authService.signupAdmin(formData).subscribe({
        next: res => {
          this.toastr.success("Registration success")
          this.router.navigate(['/login'])
          console.log(res);
        },
        error : err => {
          this.toastr.error("Registration failed")
          console.log(err);
         }
      })
    } else {
      this.toastr.error("Please provide a valid registration details");
      console.log("Form validation failed!");
    }
  }

  toggleShowConfirmPassword() {
    this.showConfirmPassword = !this.showConfirmPassword;
  }
  toggleShowPassword() {
    this.showPassword = !this.showPassword;
  }

  passwordMatchValidator() {
    const password = this?.adminForm.get('password')?.value;
    const confirmPassword = this?.adminForm.get('confirmPassword')?.value;
    return password === confirmPassword ? null : { passwordMismatch: true };
  }
}
