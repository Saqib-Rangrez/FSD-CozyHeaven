import { Component, inject } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthAPIService } from '../../../../services/operations/auth-api.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-sidebar',
  templateUrl: './sidebar.component.html',
  styleUrl: './sidebar.component.css'
})
export class SidebarComponent {

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

  ngAfterContentInit(){
    this.user = JSON.parse(localStorage.getItem('user'));
  }
}
