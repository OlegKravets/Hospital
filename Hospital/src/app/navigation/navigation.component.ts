import { Component, OnInit } from '@angular/core';
import { AccountService } from '../services/account.service';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-navigation',
  templateUrl: './navigation.component.html',
  styleUrls: ['./navigation.component.css']
})
export class NavigationComponent implements OnInit {
  model: any = {};

  constructor(
    public accountService: AccountService,
    private router: Router,
    private toastr: ToastrService) {}

  ngOnInit(): void {
  }

  login()
  {
    this.accountService.login(this.model)
      .subscribe({
        next: _ => {
          this.router.navigateByUrl('/doctors');
          this.model = {};
        },
        error: error => this.toastr.error(error.error)
      });
  }

  logout()
  {
    this.router.navigateByUrl('/');
    this.accountService.logout();
  }
}
