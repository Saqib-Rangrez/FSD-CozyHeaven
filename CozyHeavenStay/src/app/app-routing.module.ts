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

const routes: Routes = [
  {path:"" , component: HomeComponent},
  {path: "home", component : HomeComponent},
  {path: "signup-user", component: SignupUserComponent},
  {path: "signup-admin", component: SignupAdminComponent},
  {path: "login", component: LoginComponent},
  {path: "forget-password", component: ForgetPasswordComponent},
  {path: "reset-password/:token", component: ResetPasswordComponent},

  {path: "**", component: ErrorComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
