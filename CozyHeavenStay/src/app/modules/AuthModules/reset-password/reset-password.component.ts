import { Component, inject } from '@angular/core';
import { ResetPasswordDTO } from '../../../models/DTO/ResetPasswordDTO';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { AuthAPIService } from '../../../services/operations/auth-api.service';
import { ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-reset-password',
  templateUrl: './reset-password.component.html',
  styleUrl: './reset-password.component.css'
})
export class ResetPasswordComponent {
  resetForm: FormGroup;
  authService : AuthAPIService = inject(AuthAPIService);
  activeRoute: ActivatedRoute = inject(ActivatedRoute);
  toastr : ToastrService = inject(ToastrService);
  showConfirmPassword: boolean = false;
  showPassword: boolean = false;

  ngOnInit() {
    this.resetForm = new FormGroup({
      email: new FormControl(null, [Validators.required, Validators.email]),
      password: new FormControl(null,  [Validators.required]),
      confirmPassword: new FormControl(null,  [Validators.required, this.passwordMatchValidator]),
    });
  }

  onSubmit() {
    if (this.resetForm.valid) {
      const formData: ResetPasswordDTO = this.resetForm.value;
      formData.token = this.activeRoute.snapshot.paramMap.get("token");
      console.log(formData);
      // Do something with formData, like sending it to the server
      this.authService.resetPassword(formData).subscribe({
        next : (res) => {
          console.log(res);
        },
        error : (err) => {
          console.log(err);
        }
      })
      console.log(formData);
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

  passwordMatchValidator(formGroup: FormGroup) {
    const password = formGroup.get('password')?.value;
    const confirmPassword = formGroup.get('confirmPassword')?.value;
    return password === confirmPassword ? null : { passwordMismatch: true };
  }
}
