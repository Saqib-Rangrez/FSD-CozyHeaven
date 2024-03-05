import { Component, inject } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { AuthAPIService } from './services/operations/auth-api.service';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  toastr: ToastrService = inject(ToastrService);
  authService : AuthAPIService = inject(AuthAPIService);

  ngOnInit() {
    this.authService.autoLogin();
  }
}
