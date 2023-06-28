import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { User } from '../models/user';
import { BehaviorSubject, map } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  baseUrl='https://localhost:7240/api/';
  private currentUserSource = new BehaviorSubject<User | null>(null);
  currentUser$ = this.currentUserSource.asObservable();

  constructor(private http: HttpClient) { }

  login(model: any)
  {
    return this.http.post<User>(this.baseUrl + 'account/login', model).pipe(
      map((responce: User) => {
        const user = responce;
        if (user) {
          this.setCurrentUser(user);
        }
      })
    );
  }

  register (model: any)
  {
    return this.http.post<User>(this.baseUrl + 'account/register', model)
                    .pipe(map(user => { if (user) { this.setCurrentUser(user); } }))
  }

  setCurrentUser(user: User | null)
  {
    localStorage.setItem('user', JSON.stringify(user));
    this.currentUserSource.next(user);
  }

  logout()
  {
    localStorage.removeItem('user');
    this.setCurrentUser(null);
  }
}
