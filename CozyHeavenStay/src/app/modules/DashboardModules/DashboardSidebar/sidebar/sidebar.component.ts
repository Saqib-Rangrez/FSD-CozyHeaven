import { Component, inject } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthAPIService } from '../../../../services/operations/auth-api.service';
import { ToastrService } from 'ngx-toastr';
import { UserService } from '../../../../services/operations/user.service';

@Component({
  selector: 'app-sidebar',
  templateUrl: './sidebar.component.html',
  styleUrl: './sidebar.component.css'
})
export class SidebarComponent {

  authService : AuthAPIService = inject(AuthAPIService);
  toastr : ToastrService = inject(ToastrService);
  userService : UserService = inject(UserService);
  activeRoute: ActivatedRoute = inject(ActivatedRoute);
  router : Router = inject(Router);  token : string;
  user : any;
  allowEdit : boolean = true;
  

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
 
  toggleEdit() {
    this.allowEdit =!this.allowEdit;
    console.log(this.allowEdit)
    this.userService.setDynamicData(this.allowEdit);
  }

  ngAfterContentChecked(){
    this.user = JSON.parse(localStorage.getItem('user'));
  }
}
