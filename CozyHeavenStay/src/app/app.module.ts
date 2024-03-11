import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { ToastrModule } from 'ngx-toastr';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NavbarComponent } from './shared/navbar/navbar.component';
import { LoginComponent } from './modules/AuthModules/login/login.component';
import { SignupUserComponent } from './modules/AuthModules/signup-user/signup-user.component';
import { SignupAdminComponent } from './modules/AuthModules/signup-admin/signup-admin.component';
import { ForgetPasswordComponent } from './modules/AuthModules/forget-password/forget-password.component';
import { ResetPasswordComponent } from './modules/AuthModules/reset-password/reset-password.component';
import { HomeComponent } from './modules/HomeModules/home/home.component';
import { ErrorComponent } from './shared/error/error.component';
import { FooterComponent } from './shared/footer/footer.component';
import { HotelComponent } from './modules/HotelModules/hotel/hotel.component';
import { HotelcardComponent } from './modules/HotelModules/hotelcard/hotelcard.component';
// import {NgxTinySliderModule} from 'ngx-tiny-slider';
import { RatingStarsComponent } from './shared/rating-stars/rating-stars.component';
import { SidebarComponent } from './modules/DashboardModules/DashboardSidebar/sidebar/sidebar.component';
import { ProfileComponent } from './modules/DashboardModules/UserDashboard/profile/profile.component';
import { BookingsComponent } from './modules/DashboardModules/UserDashboard/bookings/bookings.component';
import { DeleteProfileComponent } from './modules/DashboardModules/UserDashboard/delete-profile/delete-profile.component';
import { BookingDetailComponent } from './modules/DashboardModules/UserDashboard/bookings/booking-detail/booking-detail.component';
import { NodataComponent } from './modules/DashboardModules/UserDashboard/bookings/nodata/nodata.component';
import { PaymentCardComponent } from './modules/DashboardModules/UserDashboard/payment-history/payment-card/payment-card.component';
import { PaymentHistoryComponent } from './modules/DashboardModules/UserDashboard/payment-history/payment-history.component';
import { AdBookingsComponent } from './modules/DashboardModules/AdminDashboard/AdminBookings/ad-bookings/ad-bookings.component';
import { AdUsersComponent } from './modules/DashboardModules/AdminDashboard/AdminUsers/ad-users/ad-users.component';
import { AdUserDetailComponent } from './modules/DashboardModules/AdminDashboard/AdminUsers/ad-user-detail/ad-user-detail.component';
import { AdOwnersComponent } from './modules/DashboardModules/AdminDashboard/AdminOwners/ad-owners/ad-owners.component';
import { AdOwnerDetailComponent } from './modules/DashboardModules/AdminDashboard/AdminOwners/ad-owner-detail/ad-owner-detail.component';
import { AdReviewComponent } from './modules/DashboardModules/AdminDashboard/AdminUsers/ad-user-detail/ad-review/ad-review.component';
import { HotelOwnComponent } from './modules/DashboardModules/AdminDashboard/AdminOwners/ad-owner-detail/hotel-own/hotel-own.component';
import { AddHotelComponent } from './modules/AddHotelModules/add-hotel/add-hotel.component';
import { AddHotelInfoComponent } from './modules/AddHotelModules/add-hotel-info/add-hotel-info.component';
import { AddRoomInfoComponent } from './modules/AddHotelModules/add-room-info/add-room-info.component';
import { AddHrFinalComponent } from './modules/AddHotelModules/add-hr-final/add-hr-final.component';
import { OwnHotelListingComponent } from './modules/DashboardModules/OwnerDashboard/own-hotel-listing/own-hotel-listing.component';
import { OwnListingDetailComponent } from './modules/DashboardModules/OwnerDashboard/own-listing-detail/own-listing-detail.component';
import { BookingRoomComponent } from './modules/BookingModule/booking-room/booking-room.component';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';
import { TimepickerModule } from 'ngx-bootstrap/timepicker';
import { MatDialogModule } from '@angular/material/dialog';
import { MatButtonModule } from '@angular/material/button';
import { NgbModule, NgbTypeaheadModule } from '@ng-bootstrap/ng-bootstrap';
import { AsyncPipe, CommonModule, JsonPipe } from '@angular/common';
import { BookingConfirmComponent } from './modules/BookingModule/booking-confirm/booking-confirm.component';
import { AboutComponent } from './modules/about/about.component';
import { ContactComponent } from './modules/contact/contact.component';
import { HotelDetailComponent } from './modules/HotelModules/hotel-detail/hotel-detail.component';
import { AuthGuard } from './guards/auth.guard';
import { JwtModule } from "@auth0/angular-jwt";
import { MatAutocompleteModule } from '@angular/material/autocomplete';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { LocationAutoComponent } from './modules/HotelModules/location-auto/location-auto.component';

export function tokenGetter() { 
  return  JSON.parse(localStorage.getItem("user")).token; 
}


@NgModule({
  declarations: [
    AppComponent,
    NavbarComponent,
    LoginComponent,
    SignupUserComponent,
    SignupAdminComponent,
    ForgetPasswordComponent,
    ResetPasswordComponent,
    HomeComponent,
    ErrorComponent,
    FooterComponent,
    HotelComponent,
    HotelcardComponent,
    RatingStarsComponent,
    SidebarComponent,
    ProfileComponent,
    BookingsComponent,
    DeleteProfileComponent,
    BookingDetailComponent,
    NodataComponent,
    PaymentHistoryComponent,
    PaymentCardComponent,
    AdBookingsComponent,
    AdUsersComponent,
    AdUserDetailComponent,
    AdOwnersComponent,
    AdOwnerDetailComponent,
    AdReviewComponent,
    HotelOwnComponent,
    AddHotelComponent,
    AddHotelInfoComponent,
    AddRoomInfoComponent,
    AddHrFinalComponent,
    OwnHotelListingComponent,
    OwnListingDetailComponent,
    BookingRoomComponent,
    BookingConfirmComponent,
    AboutComponent,
    ContactComponent,
    HotelDetailComponent,
    LocationAutoComponent,
 
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    ToastrModule.forRoot(),
    BrowserAnimationsModule,
    HttpClientModule,
    ReactiveFormsModule,
    FormsModule,
    BsDatepickerModule.forRoot(),
    TimepickerModule.forRoot(),
    MatDialogModule,
    MatButtonModule,
    NgbModule,
    CommonModule,
    MatAutocompleteModule,
    MatInputModule,
    MatFormFieldModule,
    AsyncPipe,
    NgbTypeaheadModule,
    JsonPipe,
    JwtModule.forRoot({
      config: {
        tokenGetter: tokenGetter,
        allowedDomains: ['*'],
        disallowedRoutes: []
      }
    })
  ],

  providers: [AuthGuard],
  bootstrap: [AppComponent]
})
export class AppModule { }
