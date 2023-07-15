import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-doctors',
  templateUrl: './doctors.component.html',
  styleUrls: ['./doctors.component.css']
})

export class DoctorsComponent implements OnInit{

  doctors: any;
  
  constructor(private http: HttpClient) {}

  ngOnInit(): void {
    this.getUsers();
  }

  getUsers()
  {
    this.http.get("https://localhost:7240/api/Users/Doctors")
    .subscribe(
      {
        next: (response: any) => { this.doctors = response; },
        error: (error: any) => { console.log(error); },
        complete: () => { console.log("Doctors are received!"); }
      });
  }
}
