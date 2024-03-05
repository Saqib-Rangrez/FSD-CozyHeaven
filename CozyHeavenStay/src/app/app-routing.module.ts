import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AppComponent } from './app.component';
import { SignupUserComponent } from './modules/signup-user/signup-user.component';
import { SignupAdminComponent } from './modules/signup-admin/signup-admin.component';
import { LoginComponent } from './modules/login/login.component';

const routes: Routes = [
  //{path: "", component : },
  {path: "signup-user", component: SignupUserComponent},
  {path: "signup-admin", component: SignupAdminComponent},
  {path: "login", component: LoginComponent},

  //{path: "**", component: NotFoundComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
