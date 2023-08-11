import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Doctor } from 'src/app/models/doctor';

@Component({
  selector: 'app-doctor-card',
  templateUrl: './doctor-card.component.html',
  styleUrls: ['./doctor-card.component.css']
})
export class DoctorCardComponent implements OnInit {
 @Input() currentDoctor: Doctor | undefined;

 constructor(private router: Router) {}

 ngOnInit(): void {
   
 }

 detail(doctor: Doctor) {
  this.router.navigateByUrl('/userDetail/' + doctor.name);
 }
}
