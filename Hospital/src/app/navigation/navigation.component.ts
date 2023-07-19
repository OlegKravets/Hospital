import { Component, OnInit } from '@angular/core';
import { AccountService } from '../services/account.service';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { LogginUser } from '../models/logginUser';

@Component({
  selector: 'app-navigation',
  templateUrl: './navigation.component.html',
  styleUrls: ['./navigation.component.css']
})
export class NavigationComponent implements OnInit {
  model: LogginUser = {} as LogginUser;

  constructor(
    public accountService: AccountService,
    private router: Router,
    private toastr: ToastrService) {}

  ngOnInit(): void {
  }

  login() {
    this.accountService.login(this.model)
      .subscribe({
        next: _ => {
          this.router.navigateByUrl('/doctors');
          this.model = {} as LogginUser;
        },
        error: error => this.toastr.error(error.error)
      });
  }

  logout() {
    this.router.navigateByUrl('/');
    this.accountService.logout();
  }
}
