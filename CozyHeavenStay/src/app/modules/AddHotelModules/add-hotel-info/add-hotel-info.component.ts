import { Component, Input, inject } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { HotelService } from '../../../services/hotel.service';
import { ToastrService } from 'ngx-toastr';
import {HotelDTO } from '../../../models/DTO/HotelDTO.Model'
import { INDIAN_STATES } from '../../../utils/state';

@Component({
  selector: 'app-add-hotel-info',
  templateUrl: './add-hotel-info.component.html',
  styleUrl: './add-hotel-info.component.css'
})
export class AddHotelInfoComponent {
  hotelForm: FormGroup;
  hotelService : HotelService = inject(HotelService);
  toaster : ToastrService = inject(ToastrService);
  selectedFile : File;
  imagePreviews: any[] = [];
  user : any;
  states : string[];

  ngOnInit(): void {
    this.states = INDIAN_STATES;
    this.hotelForm = new FormGroup({
      name: new FormControl('',[ Validators.required]),
      ownerId: new FormControl ('', [Validators.required]),
      amenities: new FormControl('', [ Validators.required]),
      description: new FormControl('', [ Validators.required]),
      country: new FormControl('', [ Validators.required]),
      state: new FormControl('', [ Validators.required]),
      city: new FormControl('', [ Validators.required]),
      street: new FormControl('', [ Validators.required]),
      pincode: new FormControl('', [ Validators.required]),
      files: new FormControl([null]) 
    });
  }


  onFileChange(event) {
    const files = event.target.files;
    this.processFiles(files);
  }

  removeImage(image) {
    const index = this.imagePreviews.indexOf(image);
    if (index !== -1) {
      this.imagePreviews.splice(index, 1);
    }
  }

  processFiles(files: FileList) {
    this.hotelForm.get('files').setValue(files);
    this.imagePreviews = [];

    for (let i = 0; i < files.length; i++) {
      const reader = new FileReader();
      reader.onload = () => {
        this.imagePreviews.push(reader.result);
      };
      reader.readAsDataURL(files[i]);
    }
  }


  // Function to handle form submission
  onSubmit() {

    this.user = JSON.parse(localStorage.getItem("user"))

    const address = this.hotelForm.get('street').value + ' ' + this.hotelForm.get('city').value + ' ' + this.hotelForm.get('pincode').value+
    ','+ this.hotelForm.get('state').value + ','+ this.hotelForm.get('country').value;

    const hotelData: HotelDTO = new HotelDTO(
      this.hotelForm.get('name').value,
      address,
      this.hotelForm.get('description').value,
      this.hotelForm.get('amenities').value,
      this.user.adminId,
      this.hotelForm.get('files').value
    );
    console.log("HOTEL DATA ....",hotelData)

    const formData = new FormData();
    formData.append('name', this.hotelForm.get('name').value);
    formData.append('location', address);
    formData.append('description', this.hotelForm.get('description').value);
    formData.append('amenities', this.hotelForm.get('amenities').value);
    formData.append('ownerId', this.user.adminId);

    const files = this.hotelForm.get('files').value;
    for (let i = 0; i < files.length; i++) {
      formData.append('files', files[i]);
    }

    this.hotelService.createHotel(formData).subscribe({
      next : (res) => {
        console.log(res);
        this.toaster.success("Hotel added successfully");
        this.hotelForm.reset();
      },
      error : (err) => {
        console.log(err);
      },
      complete : () => {
        this.hotelForm.reset();
        this.hotelService.hotelInfo = hotelData;
        this.hotelService.step = 2;
      }
    });
  }
}
