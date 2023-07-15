import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  registerMode = false;
  users: any;

  constructor(private http: HttpClient) {}

  ngOnInit(): void {
    this.getUsers();
  }

  registerToggle(){
    this.registerMode = !this.registerMode;
  }

  cancelRegisterMode(event: boolean) {
    this.registerMode = event;
  } 

  getUsers()
  {
    this.http.get("https://localhost:7240/api/Users")
    .subscribe(
      {
        next: (response: any) => { this.users = response; },
        error: (error: any) => { console.log(error); },
        complete: () => { console.log("Request is complete!"); }
      });
  }
}
