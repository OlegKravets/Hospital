import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.css']
})
export class UsersComponent implements OnInit {

  users: any;

  constructor(private http: HttpClient) {}

  ngOnInit(): void {
    this.getUsers();
  }

  getUsers()
  {
    this.http.get("https://localhost:7240/api/Users")
    .subscribe(
      {
        next: (response: any) => { this.users = response; },
        error: (error: any) => { console.log(error); },
        complete: () => { console.log("Doctors are received!"); }
      });
  }
}
