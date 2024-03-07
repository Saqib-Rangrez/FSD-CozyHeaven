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
import {NgxTinySliderModule} from 'ngx-tiny-slider';
import { RatingStarsComponent } from './shared/rating-stars/rating-stars.component';

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
    
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    ToastrModule.forRoot(),
    BrowserAnimationsModule,
    HttpClientModule,
    ReactiveFormsModule,
  ],

  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
