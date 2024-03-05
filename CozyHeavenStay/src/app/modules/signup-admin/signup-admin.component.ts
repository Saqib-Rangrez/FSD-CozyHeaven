import { Component, inject } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { RegisterAdminDTO } from '../../models/DTO/RegisterAdminDTO';
import { AuthAPIService } from '../../services/operations/auth-api.service';

@Component({
  selector: 'app-signup-admin',
  templateUrl: './signup-admin.component.html',
  styleUrl: './signup-admin.component.css'
})
export class SignupAdminComponent {
  adminForm: FormGroup;
  authService : AuthAPIService = inject(AuthAPIService);
  constructor(private formBuilder: FormBuilder) { }

  ngOnInit(): void {
    this.adminForm = this.formBuilder.group({
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
          console.log(res);
        },
        error : err => {
          console.log(err);
         }
      })
      // Implement your submission logic here
      console.log(formData);
    } else {
      // Handle form validation errors
      console.log("Form validation failed!");
    }
  }
}
