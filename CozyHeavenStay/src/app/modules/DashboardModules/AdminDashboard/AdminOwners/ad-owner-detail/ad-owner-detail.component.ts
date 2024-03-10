import { Component, inject } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
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
  ownerHotels : any;
  loading : boolean = false;

  ngOnInit(): void {
    this.loading = true;
    this.route.params.subscribe(params => {
      this.id = params['id'];
      console.log('ID:', this.id); 
    });

    this.ownerService.getHotelOwnerById(this.id).subscribe({
      next : res => {
        this.user = res;
        this.user = this.user.data;
        this.ownerHotels = this.user.hotels;
        console.log("OWNER DETAILS",this.user);
        console.log("OWNER HOTELS",this.ownerHotels);
      },
      error : err => {
        console.log(err);
      },
      complete : () => {
        this.loading = false;
      }
    });

  }

  deleteUser(id) {
    this.ownerService.deleteHotelOwner(id).subscribe({
      next: (res) => {
        this.router.navigate(['/dashboard/manage-owners']);
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
