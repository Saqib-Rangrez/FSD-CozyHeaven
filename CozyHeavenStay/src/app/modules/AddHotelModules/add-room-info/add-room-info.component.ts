import { Component, inject } from '@angular/core';
import { RoomDTO } from '../../../models/DTO/RoomDTO.Model';
import { RoomService } from '../../../services/room.service';
import { ToastrService } from 'ngx-toastr';
import { HotelDTO } from '../../../models/DTO/HotelDTO.Model';
import { RoomConstants } from '../../../utils/RoomConstants';
import { HotelService } from '../../../services/hotel.service';
import { FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-add-room-info',
  templateUrl: './add-room-info.component.html',
  styleUrl: './add-room-info.component.css'
})
export class AddRoomInfoComponent {
  roomForm: FormGroup;
  roomService : RoomService = inject(RoomService);
  hotelService : HotelService = inject(HotelService);
  toaster : ToastrService = inject(ToastrService);
  selectedFile : File;
  imagePreviews: any[] = [];
  user : any;
  currHotel ;
  roomConstants = RoomConstants;


constructor(private fb: FormBuilder) { }
  ngOnInit(): void {
    this.currHotel = this.hotelService.hotelInfo;
    console.log(this.currHotel)
    this.roomForm = this.fb.group({
      rooms: this.fb.array([this.createRoom()])
    });
  }

createRoom(): FormGroup {
  return this.fb.group({
    roomType: ['', Validators.required],
    maxOccupancy: ['', Validators.required],
    bedType: ['', Validators.required],
    baseFare: [ , Validators.required],
    roomSize: ['', Validators.required],
    acstatus: ['', Validators.required],
    files: [[]]
  });
}

get rooms(): FormArray {
  return this.roomForm.get('rooms') as FormArray;
}

addRoom(): void {
  this.rooms.push(this.createRoom());
}

removeRoom(index: number): void {
  this.rooms.removeAt(index);
}

onFileChange(event, roomIndex: number) {
  const files = event.target.files;
  this.processFiles(files, roomIndex);
}

removeImage(image) {
  const index = this.imagePreviews.indexOf(image);
  if (index !== -1) {
    this.imagePreviews.splice(index, 1);
  }
}

processFiles(files: FileList, roomIndex: number) {
  const roomControl = this.rooms.at(roomIndex) as FormGroup;

  roomControl.get('files').setValue(files);
  this.imagePreviews[roomIndex] = [];

  for (let i = 0; i < files.length; i++) {
    const reader = new FileReader();
    reader.onload = () => {
      this.imagePreviews[roomIndex].push(reader.result);
    };
    reader.readAsDataURL(files[i]);
  }
}

  // Function to handle form submission
  // onSubmit() {

  //   this.user = JSON.parse(localStorage.getItem("user"))
  //   console.log("USER DATA",this.user)

  //   const formData = new FormData();
  //   formData.append('hotelId', this.currHotel.hotelId);
  //   formData.append('roomType', this.roomForm.get('roomType').value);
  //   formData.append('bedType', this.roomForm.get('bedType').value);
  //   formData.append('baseFare', this.roomForm.get('baseFare').value);
  //   formData.append('roomSize', this.roomForm.get('roomSize').value);
  //   formData.append('acstatus', this.roomForm.get('acstatus').value);

  //   const files = this.roomForm.get('files').value;
  //   for (let i = 0; i < files.length; i++) {
  //     formData.append('files', files[i]);
  //   }

  //   this.hotelService.createHotel(formData).subscribe({
  //     next : (res) => {
  //       console.log(res);
  //       this.hotelService.hotelInfo = res.data;
  //       this.toaster.success("Hotel added successfully");
  //       this.roomForm.reset();
  //     },
  //     error : (err) => {
  //       console.log(err);
  //     },
  //     complete : () => {
  //       this.roomForm.reset();
  //       this.hotelService.step = 3;
  //     }
  //   });
  // }

  onSubmit(): void {
    console.log("Entered in SUbmit")
    if (true) {
      const loadingToast = this.toaster.info('Add Rooms...', 'Please wait', {
        disableTimeOut: true,
        closeButton: false,
        positionClass: 'toast-top-center'
      });
  
      this.rooms.controls.forEach((roomControl: FormGroup) => {
        const formData = new FormData();
        const roomValue = roomControl.value;
        console.log(roomValue)
  
        Object.keys(roomValue).forEach(key => {
          if (key === 'files') {
            const files = roomValue[key];
            for (let i = 0; i < files.length; i++) {
              formData.append(key, files[i]);
            }
          } else {
            formData.append(key, roomValue[key]);
          }
        });
        formData.append('hotelId', '1');
        this.user = JSON.parse(localStorage.getItem("user"))
        this.roomService.createRoom(formData,this.user.token).subscribe({
          next : (res) => {
            console.log(res);
            this.hotelService.hotelInfo = res.data;
            this.toaster.success("Hotel added successfully");
            this.roomForm.reset();
          },
          error : (err) => {
            console.log(err);
          },
          complete : () => {
            this.roomForm.reset();
            this.hotelService.step = 3;
          }
        });
      }      
      );
      this.toaster.clear();
      this.toaster.success("Rooms Added Successfully")
      
    } 
    
    else {
      // Form is invalid, display error messages or handle accordingly
    }
  }
}
