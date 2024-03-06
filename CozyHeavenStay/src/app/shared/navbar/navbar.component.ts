import { Component, OnInit, inject } from '@angular/core';
import { AuthAPIService } from '../../services/operations/auth-api.service';
import { ToastrService } from 'ngx-toastr';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.css'
})
export class NavbarComponent implements OnInit {
  authService : AuthAPIService = inject(AuthAPIService);
  toastr : ToastrService = inject(ToastrService);
  activeRoute: ActivatedRoute = inject(ActivatedRoute);
  router : Router = inject(Router);  token : string;
  user : any;

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

  ngDoCheck(){
    this.user = JSON.parse(localStorage.getItem('user'));
  }

  moveToLogin(){
    this.router.navigate(['/login']);
  }
  
  moveToSignup() {
    this.router.navigate(['/signup-user']);
  }
}
