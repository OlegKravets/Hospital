import { Component, OnInit } from '@angular/core';
import { Doctor } from '../models/doctor';
import { UsersService } from '../services/users.service';

@Component({
  selector: 'app-doctors',
  templateUrl: './doctors.component.html',
  styleUrls: ['./doctors.component.css']
})

export class DoctorsComponent implements OnInit{
  doctors: Doctor[] = [];
  
  constructor(private userService: UsersService) {}

  ngOnInit(): void {
    this.userService.getDoctors()
    .subscribe({ next: (doctors: Doctor[]) => this.doctors = doctors });
  }
}
