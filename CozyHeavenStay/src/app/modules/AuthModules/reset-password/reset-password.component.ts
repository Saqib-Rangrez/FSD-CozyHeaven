import { Component, inject } from '@angular/core';
import { ResetPasswordDTO } from '../../../models/DTO/ResetPasswordDTO';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { AuthAPIService } from '../../../services/operations/auth-api.service';
import { ActivatedRoute, Router } from '@angular/router';
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
  router : Router = inject(Router);
  showConfirmPassword: boolean = false;
  showPassword: boolean = false;

  ngOnInit() {
    this.resetForm = new FormGroup({
      email: new FormControl(null, [Validators.required, Validators.email]),
      password: new FormControl(null,  [Validators.required, Validators.minLength(6)]),
      confirmPassword: new FormControl(null,  [Validators.required]),
    }, { validators: this.passwordMatchValidator });
  }

  onSubmit() {
    if (this.resetForm.valid) {
      const formData: ResetPasswordDTO = this.resetForm.value;
      this.activeRoute.params.subscribe((params) => {
        formData.token = params['token'];        
      });     
      this.authService.resetPassword(formData).subscribe({
        next : (res) => {
          this.toastr.success('Your Password has been successfully changed!','Success');     
          this.router.navigate(['/login']);     
        },
        error : (err) => {
          this.toastr.error('Something went wrong ');
          console.log(err);
        }
      })
    } else {
      this.toastr.error("Please provide a valid registration details");
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
