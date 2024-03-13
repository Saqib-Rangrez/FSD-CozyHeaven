import { Component, inject } from '@angular/core';
import { UserService } from '../../../../../services/operations/user.service';

@Component({
  selector: 'app-ad-users',
  templateUrl: './ad-users.component.html',
  styleUrl: './ad-users.component.css'
})
export class AdUsersComponent {
  userList;
  user;
  loading : boolean = false
  userService : UserService = inject(UserService);

  ngOnInit() {
    this.user = JSON.parse(localStorage.getItem('user'));

    this.loading = true;
    this.userService.getAllUsers(this.user?.token).subscribe({
      next : res => {
        this.userList = res;
      },
      error : err =>{
        this.loading = false;
        console.log(err)
      } ,
      complete : () => {
        this.loading = false;
      }
    })

  }

}
