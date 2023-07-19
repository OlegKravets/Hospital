import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { AccountService } from '../services/account.service';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Role } from '../models/role';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  @Output() cancelRegister = new EventEmitter();
  model: any = {};
  roles: Role[] = [];
  validationErrors: string[] | undefined;

  constructor(private accountService: AccountService, private http: HttpClient) {}

  ngOnInit(): void {
    this.loadRoles();
  }

  register() {
    this.accountService.register(this.model)
      .subscribe({
          next : _ => { this.cancelRegister.emit(false); },
          error: error => { this.validationErrors = error }
      })
  }

  cancel() {
    console.log("Cancelled");
    this.cancelRegister.emit(false);
  }

  loadRoles() {
    this.http.get<Role[]>(environment.apiUrl + "Roles")
    .subscribe(
      {
        next: roles => { this.roles = roles; },
        error: (error: any) => { console.log(error); },
        complete: () => { console.log("Roles are received!"); }
      }
    )
  }
}
