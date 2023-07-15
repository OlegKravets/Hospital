import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { AccountService } from '../services/account.service';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  @Output() cancelRegister = new EventEmitter();
  model: any = {}
  roles: any;
  validationErrors: string[] | undefined;

  constructor(private accountService: AccountService, private router: Router, private http: HttpClient) {}

  ngOnInit(): void {
    this.loadRoles();
  }

  register() {
    this.accountService.register(this.model)
      .subscribe({
          next : () => {
            this.router.navigateByUrl('/doctors');
          },
          error: error => {
            this.validationErrors = error
          }
      })
  }

  cancel() {
    console.log("Cancelled");
    this.cancelRegister.emit(false);
  }

  loadRoles() {
    this.http.get("https://localhost:7240/api/Roles")
    .subscribe(
      {
        next: (response: any) => { this.roles = response; },
        error: (error: any) => { console.log(error); },
        complete: () => { console.log("Roles are received!"); }
      }
    )
  }
}
