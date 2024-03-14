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
      password: new FormControl(null, [ Validators.required, Validators.minLength(6)]),
      confirmPassword: new FormControl(null, [ Validators.required])
    }, {validators : this.passwordMatchValidator} 
     );
  }


  calcStrength(): void {
    const passwordControl = this.adminForm.get('password');
    if (passwordControl && passwordControl.value) {
      const password = passwordControl.value;
      const passwordLength = password.length;
      let strengthLevel = '';
  
      const hasUpperCase = /[A-Z]/.test(password);
      const hasLowerCase = /[a-z]/.test(password);
      const hasNumber = /[0-9]/.test(password);
      const hasSymbol = /[!@#$%^&*()_+\-=\[\]{};':"\\|,.<>\/?]/.test(password);
  
      if (passwordLength >= 8 && hasUpperCase && hasLowerCase && hasNumber && hasSymbol) {
        strengthLevel = 'Strong';
        this.setIndicator('#90EE90', '0 0 5px 0.5px #0f0'); 
      } else if (passwordLength >= 6 && ((hasUpperCase && hasLowerCase) || (hasLowerCase && hasNumber) || (hasUpperCase && hasNumber) || (hasUpperCase && hasSymbol) || (hasLowerCase && hasSymbol) || (hasNumber && hasSymbol))) {
        strengthLevel = 'Medium';
        this.setIndicator('#ff0', '0 0 5px 0.5px #ff0'); 
      } else {
        strengthLevel = 'Weak';
        this.setIndicator('#FF474C', '0 0 5px 0.5px #f00'); 
      }
  
      // Update strength text based on the level
      this.updateStrengthText(strengthLevel);
    }
  }
  
  updateStrengthText(level: string): void {
    const strengthElement = document.getElementById('password-strength');
    if (strengthElement) {
      strengthElement.innerText = `Password Strength: ${level}`;
    }
  }
  

  indicatorColor: string = '';
  indicatorShadow: string = '';

  setIndicator(color: string, shadow: string): void {
    this.indicatorColor = color;
    this.indicatorShadow = shadow;
  }

  onSubmit() {
    if (this.adminForm.valid) {
      const formData: RegisterAdminDTO = this.adminForm.value;

      this.authService.signupAdmin(formData).subscribe({
        next: res => {
          this.toastr.success("Registration success")
          this.router.navigate(['/login'])
        },
        error : err => {
          this.toastr.error("Registration failed")
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
