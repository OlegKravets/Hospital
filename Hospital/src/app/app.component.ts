import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'Doctors';
  doctors: any;

  constructor(private http: HttpClient) {}

  ngOnInit() : void {
    this.http.get("https://localhost:7240/api/Users")
                .subscribe(
                  {
                    next: (response: any) => { this.doctors = response; },
                    error: (error: any) => { console.log(error); },
                    complete: () => { console.log("Request is complete!"); }
                  });
  }
}