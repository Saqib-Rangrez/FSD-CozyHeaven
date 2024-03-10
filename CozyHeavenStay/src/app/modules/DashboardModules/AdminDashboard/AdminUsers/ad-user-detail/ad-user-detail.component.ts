import { Component, inject } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { UserService } from '../../../../../services/operations/user.service';
import { ToastrService } from 'ngx-toastr';
import { ReviewService } from '../../../../../services/review.service';

@Component({
  selector: 'app-ad-user-detail',
  templateUrl: './ad-user-detail.component.html',
  styleUrl: './ad-user-detail.component.css'
})
export class AdUserDetailComponent {
  route : ActivatedRoute = inject(ActivatedRoute);
  id : number = null;
  userService : UserService = inject(UserService);
  toastr : ToastrService = inject(ToastrService);
  router : Router = inject(Router);
  reviewService : ReviewService = inject(ReviewService);
  user;
  reviews;
  loading : boolean = false;

  ngOnInit(): void {
    this.loading = true;
    this.route.params.subscribe(params => {
      this.id = params['id'];
      console.log('ID:', this.id); 
    });

    this.userService.getUserById(this.id).subscribe({
      next : res => {
        this.user = res.data;
        console.log(this.user);
      },
      error : err => {
        console.log(err);
      },
      complete : () => {
        this.loading = false;
      }
    });

    this.reviewService.getReviewByUserId(this.id).subscribe({
      next : res => {
        this.reviews = res.data;
        console.log(this.reviews);
      },
      error : err => {
        console.log(err);
      },
      complete : () => {
        this.loading = false;
      }
    })

  }

  deleteUser() {
    this.userService.deleteUser(this.user?.userId).subscribe({
      next: (res) => {
        this.router.navigate(['/dashboard/manage-users']);
        this.toastr.success("User deleted successfully")
        console.log(res);
      },
      error: (err) => {
        console.log("Failed to delete user");
        this.toastr.error("Failed to delete user");
      }
    })
  }
}
