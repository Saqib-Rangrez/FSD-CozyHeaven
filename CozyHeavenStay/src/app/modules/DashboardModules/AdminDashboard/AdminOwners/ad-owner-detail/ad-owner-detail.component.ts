import { Component, inject } from '@angular/core';
import { ActivatedRoute, NavigationEnd, Router } from '@angular/router';
import { HotelOwnerService } from '../../../../../services/hotel-owner.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-ad-owner-detail',
  templateUrl: './ad-owner-detail.component.html',
  styleUrl: './ad-owner-detail.component.css'
})
export class AdOwnerDetailComponent {
  route : ActivatedRoute = inject(ActivatedRoute);
  id : number = null;
  ownerService : HotelOwnerService = inject(HotelOwnerService);
  toastr : ToastrService = inject(ToastrService);
  router : Router = inject(Router);
  user : any;
  ownerData : any;
  ownerHotels : any;
  loading : boolean = false;

  ngOnInit(): void {
    this.loading = true;
    this.route.params.subscribe(params => {
      this.id = params['id'];
    });
    this.user = JSON.parse(localStorage.getItem("user"));
    this.ownerService.getHotelOwnerById(this.id,this.user.token).subscribe({
      next : res => {
        this.ownerData = res;
        this.ownerData = this.ownerData.data;
        this.ownerHotels = this.ownerData.hotels;
      },
      error : err => {
        console.log(err);
      },
      complete : () => {
        this.loading = false;
      }
    });

    this.router.events.subscribe((event) => {
      if (event instanceof NavigationEnd) {
        window.scrollTo(0, 0);
      }
    }); 
  }

  checkDel(value: boolean) {
    if(value) {
      this.ngOnInit();
    }
  }

  deleteUser() {
    this.ownerService.deleteHotelOwner(this.ownerData.ownerId,this.user.token).subscribe({
      next: (res) => {
        this.router.navigate(['/dashboard/manage-owners']);
        this.toastr.success("User deleted successfully")
      },
      error: (err) => {
        console.log("Failed to delete user");
        this.toastr.error("Failed to delete user");
      }
    })
  }
}
