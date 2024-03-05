import { Component, inject } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { AuthAPIService } from '../../services/operations/auth-api.service';
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


  ngOnInit(): void {
    this.loginForm = new FormGroup({
      email: new FormControl(null, [Validators.required, Validators.email]),
      password: new FormControl (null, [Validators.required])
    });
  }

  onSubmit() {
    if (this.loginForm.valid) {
      // Implement your login logic here
      const email = this.loginForm.value.email;
      const password = this.loginForm.value.password;
      console.log("Email:", email);
      console.log("Password:", password);

      this.authService.loginUser(email, password).subscribe({
        next : (res) => {
          console.log(res);
          this.toastr.success("Login success")
        },
        error: (err) => {
          console.log(err);
        }
      });


    } else {
      // Form is invalid, handle accordingly
    }
  }
}