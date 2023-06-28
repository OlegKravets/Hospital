import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { AccountService } from '../services/account.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  @Output() cancelRegister = new EventEmitter();
  model: any = {}
  validationErrors: string[] | undefined;

  constructor(private accountService: AccountService, private router: Router) {}

  ngOnInit(): void {
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
}
