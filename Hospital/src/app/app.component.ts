import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { AccountService } from './services/account.service';
import { User } from './models/user';
import { JsonPipe } from '@angular/common';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'Doctors';
  doctors: any;

  constructor(private http: HttpClient, private accoutService: AccountService) {}

  ngOnInit() : void {
    this.getUsers();
    this.setCurrentUser();
  }

  getUsers()
  {
    this.http.get("https://localhost:7240/api/Users")
    .subscribe(
      {
        next: (response: any) => { this.doctors = response; },
        error: (error: any) => { console.log(error); },
        complete: () => { console.log("Request is complete!"); }
      });
  }

  setCurrentUser()
  {
    const userStr = localStorage.getItem('user');
    if (!userStr)
      return;

    const user: User = JSON.parse(userStr);
    this.accoutService.setCurrentUser(user);
  }
}