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
    });
  }

  onSubmit() {
    if (this.loginForm.valid) {
      const email = this.loginForm.value.email;
      const password = this.loginForm.value.password;

      this.authService.login(email, password).subscribe({
        next : (res) => {
          this.toastr.success("Login success");
          this.router.navigate(['/home']);
        },
        error: (err) => {
          this.toastr.error("Invalid credentials");
        }
      })      
    } else {
      this.toastr.error("Please provide a valid credential details");
    }
  }

  toggleShowPassword() {
    this.showPassword = !this.showPassword;
  }
}