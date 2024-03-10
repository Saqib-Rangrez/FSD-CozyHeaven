import { Component, inject } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { UserService } from '../../../../services/operations/user.service';
import { AdminService } from '../../../../services/operations/admin.service';
import { HotelOwnerService } from '../../../../services/hotel-owner.service';

@Component({
  selector: 'app-delete-profile',
  templateUrl: './delete-profile.component.html',
  styleUrl: './delete-profile.component.css'
})
export class DeleteProfileComponent {
  deleteForm : FormGroup;
  toastr : ToastrService = inject(ToastrService);
  userService : UserService = inject(UserService);
  adminService : AdminService = inject(AdminService);
  ownerService : HotelOwnerService = inject(HotelOwnerService);
  user : any;

  ngOnInit () {
    this.deleteForm = new FormGroup({
      confirmChoice : new FormControl(false, [Validators.required])
    });

    this.user = JSON.parse(localStorage.getItem('user'));
  }

  onSubmit() {
    if (this.deleteForm.valid){
      if(this.user?.role === 'User'){
        this.userService.deleteUser(this.user.id).subscribe({
          next: (res) => {
            this.toastr.success("User deleted successfully")
          },
          error : (err) => {
            this.toastr.error("User delete failed")
            console.log(err);
          }
        });
      }else if(this.user?.role === 'Admin'){
        this.adminService.deleteUser(this.user.id).subscribe({
          next: (res) => {
            this.toastr.success("User deleted successfully")
          },
          error : (err) => {
            this.toastr.error("User delete failed")
            console.log(err);
          }
        });
      }else{
        this.ownerService.deleteHotelOwner(this.user.id,this.user.token).subscribe({
          next: (res) => {
            this.toastr.success("User deleted successfully")
          },
          error : (err) => {
            this.toastr.error("User delete failed")
            console.log(err);
          }
        });
      }      
    }
  }

}
