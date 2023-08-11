import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DoctorsComponent } from './doctors/doctors.component';
import { HomeComponent } from './home/home.component';
import { AuthGuard } from './guards/auth.guard';
import { UsersComponent } from './users/users.component';
import { UserDetailComponent } from './users/user-detail/user-detail.component';
import { userDetailedResolver } from './resolvers/user-detailed.resolver';

const routes: Routes = [
  {path: '', component: HomeComponent},
  {path: '', 
    runGuardsAndResolvers: 'always',
    canActivate: [AuthGuard],
    children: [
      {path: 'doctors', component: DoctorsComponent, canActivate: [AuthGuard]},
      {path: 'users', component: UsersComponent, canActivate: [AuthGuard]},
      {path: 'userDetail/:name', component: UserDetailComponent, canActivate: [AuthGuard], resolve: {user: userDetailedResolver}},
      {path: 'userDetail', component: UserDetailComponent, canActivate: [AuthGuard]},
    ]
  },
  {path: '**', component: HomeComponent, pathMatch: 'full'},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
