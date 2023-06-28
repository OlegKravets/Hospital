import { Component, OnInit } from '@angular/core';
import { AccountService } from './services/account.service';
import { User } from './models/user';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'Doctors';

  constructor(private accoutService: AccountService) {}

  ngOnInit() : void
  {
    this.setCurrentUser();
  }

  setCurrentUser()
  {
    const userStr = localStorage.getItem('user');
    if (!userStr) return;

    const user: User = JSON.parse(userStr);
    this.accoutService.setCurrentUser(user);
  }
}