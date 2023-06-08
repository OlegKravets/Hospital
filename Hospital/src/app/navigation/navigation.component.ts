import { Component, OnInit } from '@angular/core';
import { AccountService } from '../services/account.service';
import { User } from '../models/user';

@Component({
  selector: 'app-navigation',
  templateUrl: './navigation.component.html',
  styleUrls: ['./navigation.component.css']
})
export class NavigationComponent implements OnInit {
  model: any = {};
  loggedIn = false;

  constructor(private accountService: AccountService) {}

  ngOnInit(): void {
  }

  getCurrentUser()
  {
    this.accountService.currentUser$.subscribe({
      next: user => this.loggedIn = !user,
      error: error => console.log(error)
    });
  }

  login()
  {
    this.accountService.login(this.model)
      .subscribe({
        next: response => {
          console.log(response);
          this.loggedIn = true;
        },
        error: error => console.log(error)
      });
  }

  logout()
  {
    this.accountService.logout();
    this.loggedIn = false;
  }
}
