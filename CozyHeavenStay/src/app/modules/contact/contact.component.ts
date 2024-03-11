import { Component, inject } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NavigationEnd, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-contact',
  templateUrl: './contact.component.html',
  styleUrl: './contact.component.css'
})
export class ContactComponent {
  contactForm: FormGroup;
  toastr : ToastrService = inject(ToastrService);
  constructor(private fb: FormBuilder) { }

  router : Router = inject(Router);

  ngOnInit(): void {
    this.contactForm = this.fb.group({
      name: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      mobile: ['', Validators.required],
      message: ['', Validators.required],
      agree: [false, Validators.requiredTrue]
    });

    this.router.events.subscribe((event) => {
      if (event instanceof NavigationEnd) {
        window.scrollTo(0, 0);
      }
    });
  }

  onSubmit() {
    if (this.contactForm.valid) {
      // Handle form submission logic here
      console.log(this.contactForm.value);
      // Reset the form after successful submission
      this.contactForm.reset();
      this.toastr.success("Message Sent!!!");
    } else {
      // Mark fields as touched to show validation errors
      this.contactForm.markAllAsTouched();
      this.toastr.error("Invaild Form !! Please fill all details!!");
    }
  }
}
