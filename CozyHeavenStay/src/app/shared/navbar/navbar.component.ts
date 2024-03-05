import { Component, inject } from '@angular/core';
import { AuthAPIService } from '../../services/operations/auth-api.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.css'
})
export class NavbarComponent {
  authService : AuthAPIService = inject(AuthAPIService);

  logOut() {
    const user = JSON.parse(localStorage.getItem('user'))
    console.log(user);
    this.authService.logout(user?.token).subscribe({
      next : res => console.log(res),
      error : err => console.log(err)
    });
  }
}
