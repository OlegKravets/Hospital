import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { User } from 'src/app/models/user';
import { AccountService } from 'src/app/services/account.service';
import { UsersService } from 'src/app/services/users.service';

@Component({
  selector: 'app-user-detail',
  templateUrl: './user-detail.component.html',
  styleUrls: ['./user-detail.component.css']
})
export class UserDetailComponent implements OnInit {
  currentUser: User | null;

  constructor(private accountService : AccountService, private route: ActivatedRoute) {
  }

  ngOnInit(): void {
    this.route.data.subscribe({ next: data => this.currentUser = data['user'] });
    
    if (!this.currentUser) {
      this.accountService.currentUser$.subscribe( {next: user => { this.currentUser = user; }} )
    }
  }
 }