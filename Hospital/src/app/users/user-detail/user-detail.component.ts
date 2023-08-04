import { Component, Input, OnInit } from '@angular/core';
import { User } from 'src/app/models/user';
import { AccountService } from 'src/app/services/account.service';

@Component({
  selector: 'app-user-detail',
  templateUrl: './user-detail.component.html',
  styleUrls: ['./user-detail.component.css']
})
export class UserDetailComponent implements OnInit {
  currentUser: User | null;

  constructor(private accountService : AccountService) {}
 
  ngOnInit(): void {
    this.accountService.currentUser$
      .subscribe({next: user => { this.currentUser = user; }})
  }
 }