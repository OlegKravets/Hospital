import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { LogginUser } from '../models/logginUser';
import { BehaviorSubject, map } from 'rxjs';
import { environment } from 'src/environments/environment';
import { User } from '../models/user';

@Injectable({
  providedIn: 'root'
})

export class AccountService {
  private currentUserSource = new BehaviorSubject<User | null>(null);
  currentUser$ = this.currentUserSource.asObservable();

  constructor(private http: HttpClient) { }

  login(model: LogginUser)
  {
    return this.http.post<User>(environment.apiUrlAccount + 'login', model).pipe(
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
    return this.http.post<User>(environment.apiUrlAccount + 'register', model)
                    .pipe(map(user =>
                      {
                        if (user) {
                          this.setCurrentUser(user);
                        }
                      }))
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
