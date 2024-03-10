import { Component, Input, inject } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { AuthAPIService } from '../../../../services/operations/auth-api.service';
import { UserService } from '../../../../services/operations/user.service';
import { User } from '../../../../models/User.Model';
import { Admin } from '../../../../models/Admin.Model';
import { HotelOwner } from '../../../../models/hotel-owner.Model';
import { HotelOwnerService } from '../../../../services/hotel-owner.service';
import { AdminService } from '../../../../services/operations/admin.service';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrl: './profile.component.css'
})
export class ProfileComponent {
  toastr : ToastrService = inject(ToastrService);
  authService : AuthAPIService = inject(AuthAPIService);
  userService : UserService = inject(UserService);
  adminService : AdminService = inject(AdminService);
  ownerService : HotelOwnerService = inject(HotelOwnerService);
  showConfirmPassword: boolean = false;
  showPassword: boolean = false;
  userForm: FormGroup;
  userToUpdate : User;
  adminToUpdate : Admin;
  ownerToUpdate : HotelOwner;
  user : any;
  selectedFile : File;
  imagePreview: string | ArrayBuffer = null; 
  allowEdit : boolean = true;


  ngOnInit() {
    this.user = JSON.parse(localStorage.getItem('user'));

    this.userService.dynamicData$.subscribe(data => {
      this.allowEdit = data;
      console.log(data)
    });

    this.userForm = new FormGroup({
      userId: new FormControl(null, Validators.required),
      firstName: new FormControl(null, Validators.required),
      lastName: new FormControl(null, Validators.required),
      email: new FormControl(null, [Validators.required, Validators.email]),
      password: new FormControl(null, [Validators.required, Validators.minLength(3)]),
      confirmPassword: new FormControl(null, [Validators.required, this.passwordMatchValidator, Validators.minLength(3)]),
      gender: new FormControl(null, Validators.required),
      contactNumber: new FormControl(null, [Validators.required ,Validators.pattern(/^\d{10}$/)]),
      address: new FormControl(null, Validators.required),
      role: new FormControl(null),
      profileImage: new FormControl(null),
      token: new FormControl(null),
      resetPasswordExpires: new FormControl(null)
    });
    
    this.userForm.patchValue({
      address: this.user?.address,
      confirmPassword : null,
      contactNumber: this.user?.contactNumber,
      email: this.user?.email,      
      firstName: this.user?.firstName,
      gender: this.user?.gender,
      lastName: this.user?.lastName,      
      password : null,      
      profileImage: this.user?.profileImage,
      resetPasswordExpires: '',      
      role: this.user?.role,      
      token: this.user?.token,      
      userId: this.user?.userId,
    })

    console.log(this.userForm.value)
    
  }

  updateProfile() {

    if(this.user?.role == 'User') {
      this.userToUpdate = new User(
        this.userForm.value.userId,
        this.userForm.value.firstName,
        this.userForm.value.lastName,
        this.userForm.value.email,
        this.userForm.value.password,
        this.userForm.value.gender,
        this.userForm.value.contactNumber,
        this.userForm.value.address,
        this.userForm.value.role,
        this.userForm.value.profileImage,
        this.userForm.value.token,
        this.userForm.value.resetPasswordExpires
      );

      this.userService.updateUser(this.userToUpdate, this.user.token).subscribe({
        next : (res) => {
          this.toastr.success("User updated successfully")
        },
        error : (err) => {
          this.toastr.error("User update failed")
          console.log(err);
        }
      })

    }else if(this.user?.role == 'Admin') {
      this.adminToUpdate = new Admin(
        this.userForm.value.userId,
        this.userForm.value.firstName,
        this.userForm.value.lastName,
        this.userForm.value.email,
        this.userForm.value.password,
        this.userForm.value.profileImage,
        this.userForm.value.role,
        this.userForm.value.token,
        this.userForm.value.resetPasswordExpires
      );
      console.log(this.adminToUpdate)

      this.adminService.updateAdmin(this.adminToUpdate, this.user.token).subscribe({
        next : (res) => {
          this.toastr.success("User updated successfully")
        },
        error : (err) => {
          this.toastr.error("User update failed")
          console.log(err);
        }
      })
    }else{
      this.ownerToUpdate = new HotelOwner(
        this.userForm.value.userId,
        this.userForm.value.firstName,
        this.userForm.value.lastName,
        this.userForm.value.email,
        this.userForm.value.password,
        this.userForm.value.gender,
        this.userForm.value.contactNumber,
        this.userForm.value.address,
        this.userForm.value.profileImage,
        this.userForm.value.role,
        this.userForm.value.token,
        this.userForm.value.resetPasswordExpires
      );

      this.ownerService.updateHotelOwner(this.ownerToUpdate,this.user.token).subscribe({
        next : (res) => {
          this.toastr.success("User updated successfully")
        },
        error : (err) => {
          this.toastr.error("User update failed")
          console.log(err);
        }
      })
    }
  }

  toggleShowConfirmPassword() {
    this.showConfirmPassword = !this.showConfirmPassword;
  }
  toggleShowPassword() {
    this.showPassword = !this.showPassword;
  }

  

  onFileSelected(event: any) {
    this.selectedFile = event.target.files[0] as File;
    this.showImagePreview(this.selectedFile);
  }

   // Show image preview
   showImagePreview(file: File) {
    const reader = new FileReader();
    reader.onload = () => {
      this.imagePreview = reader.result;
    };
    reader.readAsDataURL(file);
  }

  onUpload() {
    if (this.selectedFile) {
      const formData = new FormData();
    
      formData.append('file', this.selectedFile);
      console.log(this.selectedFile);
      let id : number;
      if(this.user?.role === 'User'){
        formData.append('id', this.user?.userId);
        formData.append('Role', this.user?.role);
      }else if(this.user?.role === 'Admin') {
        formData.append('id', this.user?.adminId);
        formData.append('Role', this.user?.role);
      }else{
        formData.append('id', this.user?.userId);
        formData.append('Role', this.user?.role);
      }

      this.userService.uploadDisplayPicture(formData, this.user.token).subscribe({
        next : (res) => {
          console.log(res);
          this.toastr.success("Image Uploaded Successfully");
        },
        error : (err) => {
          console.log(err);
          this.toastr.error("Image Uploading Failed");
        }
      });

    }
  }

  passwordMatchValidator() {
    const password = this?.userForm.get('password')?.value;
    const confirmPassword = this?.userForm.get('confirmPassword')?.value;
    return password === confirmPassword ? null : { passwordMismatch: true };
  }

}
