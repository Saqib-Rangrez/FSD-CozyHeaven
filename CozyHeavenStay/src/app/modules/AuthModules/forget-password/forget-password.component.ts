import { Component, inject } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { AuthAPIService } from '../../../services/operations/auth-api.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-forget-password',
  templateUrl: './forget-password.component.html',
  styleUrl: './forget-password.component.css'
})
export class ForgetPasswordComponent {
  loginForm: FormGroup;
  authService : AuthAPIService = inject(AuthAPIService);
  toastr : ToastrService = inject(ToastrService);


  ngOnInit(): void {
    this.loginForm = new FormGroup({
      email: new FormControl(null, [Validators.required, Validators.email]),
    });
  }

  onSubmit() {
    if (this.loginForm.valid) {
      // Implement your login logic here
      const email = this.loginForm.value.email;
      console.log("Email:", email);

      this.authService.forgetPassword(email).subscribe({
        next : (res) => {
          console.log(res);
          this.toastr.success("Email sent successfully")
        },
        error: (err) => {
          this.toastr.error("Failed to send email");
          console.log(err);
        }
      });
    } else {
      // Form is invalid, handle accordingly
    }
  }
}
