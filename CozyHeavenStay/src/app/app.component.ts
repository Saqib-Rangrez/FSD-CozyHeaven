import { Component, inject } from '@angular/core';
import { ToastrService } from 'ngx-toastr';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'CozyHeavenStay';
  constructor(private toastr: ToastrService) {}


  ngOnInit() {
    this.toastr.info('Loading...', 'Please wait', {
      disableTimeOut: true,
      closeButton: false,
      positionClass: 'toast-top-center'
    });

    // Simulate loading delay (e.g., making HTTP request)
    setTimeout(() => {
      // Hide the loading toast
      this.toastr.clear();
      
      // Display success or other type of toast
      this.toastr.success('Data loaded successfully', 'Success');
    }, 2000);   }
}
