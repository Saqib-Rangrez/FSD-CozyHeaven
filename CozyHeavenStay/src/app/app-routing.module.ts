import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AppComponent } from './app.component';
import { SignupUserComponent } from './modules/AuthModules/signup-user/signup-user.component';
import { SignupAdminComponent } from './modules/AuthModules/signup-admin/signup-admin.component';
import { LoginComponent } from './modules/AuthModules/login/login.component';
import { ForgetPasswordComponent } from './modules/AuthModules/forget-password/forget-password.component';
import { ResetPasswordComponent } from './modules/AuthModules/reset-password/reset-password.component';
import { HomeComponent } from './modules/HomeModules/home/home.component';
import { ErrorComponent } from './shared/error/error.component';
import { HotelComponent } from './modules/HotelModules/hotel/hotel.component';
import { SidebarComponent } from './modules/DashboardModules/DashboardSidebar/sidebar/sidebar.component';
import { BookingsComponent } from './modules/DashboardModules/UserDashboard/bookings/bookings.component';
import { ProfileComponent } from './modules/DashboardModules/UserDashboard/profile/profile.component';
import { DeleteProfileComponent } from './modules/DashboardModules/UserDashboard/delete-profile/delete-profile.component';
import { PaymentHistoryComponent } from './modules/DashboardModules/UserDashboard/payment-history/payment-history.component';
import { AdBookingsComponent } from './modules/DashboardModules/AdminDashboard/AdminBookings/ad-bookings/ad-bookings.component';
import { AdUsersComponent } from './modules/DashboardModules/AdminDashboard/AdminUsers/ad-users/ad-users.component';
import { AdOwnersComponent } from './modules/DashboardModules/AdminDashboard/AdminOwners/ad-owners/ad-owners.component';
import { AdUserDetailComponent } from './modules/DashboardModules/AdminDashboard/AdminUsers/ad-user-detail/ad-user-detail.component';
import { AdOwnerDetailComponent } from './modules/DashboardModules/AdminDashboard/AdminOwners/ad-owner-detail/ad-owner-detail.component';
import { AddHotelComponent } from './modules/AddHotelModules/add-hotel/add-hotel.component';
import { OwnHotelListingComponent } from './modules/DashboardModules/OwnerDashboard/own-hotel-listing/own-hotel-listing.component';
import { BookingRoomComponent } from './modules/BookingModule/booking-room/booking-room.component';
import { BookingConfirmComponent } from './modules/BookingModule/booking-confirm/booking-confirm.component';
import { ContactComponent } from './modules/contact/contact.component';
import { AboutComponent } from './modules/about/about.component';
import { HotelDetailComponent } from './modules/HotelModules/hotel-detail/hotel-detail.component';
import { AuthGuard, OpenRoute } from './guards/auth.guard';

const routes: Routes = [
  {path:"" , component: HomeComponent},
  {path: "home", component : HomeComponent},
  {path: "hotel", component : HotelComponent, canActivate: [AuthGuard]},
  {path: "hoteldetail/:id", component : HotelDetailComponent , canActivate: [AuthGuard]},
  {path: "roombooking/:roomid", component: BookingRoomComponent , canActivate: [AuthGuard]},
  {path: "confirm/:bookingid", component: BookingConfirmComponent , canActivate: [AuthGuard]},
  {path: "contact", component: ContactComponent },
  {path: "about", component: AboutComponent },
  {path: "signup-user", component: SignupUserComponent, canActivate:[OpenRoute]},
  {path: "signup-admin", component: SignupAdminComponent, canActivate:[OpenRoute]},
  {path: "login", component: LoginComponent, canActivate:[OpenRoute]},
  {path: "forget-password", component: ForgetPasswordComponent, canActivate:[OpenRoute]},
  {path: "reset-password/:token", component: ResetPasswordComponent, canActivate:[OpenRoute]},
  {
    path: 'dashboard',
    component: SidebarComponent, canActivate: [AuthGuard],
    children: [
      { path: '', redirectTo: 'dashboard', pathMatch: 'full' },
      { path: 'bookings', component: BookingsComponent },
      { path: 'profile', component: ProfileComponent },
      { path: 'payment-history', component: PaymentHistoryComponent },
      { path: 'delete-profile', component: DeleteProfileComponent },
      { path: 'manage-bookings', component: AdBookingsComponent },
      { path: 'manage-users', component: AdUsersComponent },
      { path: 'manage-owners', component: AdOwnersComponent },
      { path: 'my-listings', component: OwnHotelListingComponent },
    ]
  },
  { path: 'manage-owners/detail/:id', component: AdOwnerDetailComponent , canActivate: [AuthGuard] },
  { path: 'manage-users/detail/:id', component: AdUserDetailComponent , canActivate: [AuthGuard] },
  { path: 'add-hotel', component: AddHotelComponent, canActivate: [AuthGuard]},
  {path: "**", component: ErrorComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
