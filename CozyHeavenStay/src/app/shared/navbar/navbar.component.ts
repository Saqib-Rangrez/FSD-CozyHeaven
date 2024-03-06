import { Component, inject } from '@angular/core';
import { AuthAPIService } from '../../services/operations/auth-api.service';
import { ToastrService } from 'ngx-toastr';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.css'
})
export class NavbarComponent {
  authService : AuthAPIService = inject(AuthAPIService);
  toastr : ToastrService = inject(ToastrService);
  activeRoute: ActivatedRoute = inject(ActivatedRoute);
  token : string;

  logOut() {
    const user = JSON.parse(localStorage.getItem('user'))
    console.log(user);
    this.authService.logout(user?.token).subscribe({
      next : res => {
        console.log(res);
        this.toastr.success("Logged out successfully", "Success");
      }  ,
      error : err => console.log(err)
    });
  }

  ngOnInit() {
    this.activeRoute.paramMap.subscribe(params => {
      this.token = params.get('token');
    });
  }
  
}
