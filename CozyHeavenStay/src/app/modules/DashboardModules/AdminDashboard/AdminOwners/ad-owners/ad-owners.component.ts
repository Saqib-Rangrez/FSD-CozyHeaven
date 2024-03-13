import { Component, inject } from '@angular/core';
import { HotelOwnerService } from '../../../../../services/hotel-owner.service';

@Component({
  selector: 'app-ad-owners',
  templateUrl: './ad-owners.component.html',
  styleUrl: './ad-owners.component.css'
})
export class AdOwnersComponent {
  ownerList;
  user;
  loading : boolean = false
  ownerService : HotelOwnerService = inject(HotelOwnerService);

  ngOnInit() {
    this.user = JSON.parse(localStorage.getItem('user'));

    this.loading = true;
    this.ownerService.getAllHotelOwners(this.user.token).subscribe({
      next : res => {
        this.ownerList = res;
        this.ownerList = this.ownerList.data;
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
