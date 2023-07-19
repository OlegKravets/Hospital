import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { User } from '../models/user';
import { Doctor } from '../models/doctor';

@Injectable({
  providedIn: 'root'
})

export class UsersService {

  constructor(private http: HttpClient) { }

  getUsers() {
    return this.http.get<User[]>(environment.apiUrlUsers, this.getHttpOptions());
  }

  getUser(userName: string) {
    return this.http.get<User>(environment.apiUrlUsers + userName, this.getHttpOptions());
  }

  getDoctors() {
    return this.http.get<Doctor[]>(environment.apiUrlUsers + 'Doctors', this.getHttpOptions());
  }

  getHttpOptions() {
    const userString = localStorage.getItem('user');
    if (userString == null)
      return;

    const user = JSON.parse(userString);
    return {
      headers: new HttpHeaders({
        Authorization: 'Bearer ' + user.token
      })
    }
  }
}
