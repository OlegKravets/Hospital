import { ResolveFn } from '@angular/router';
import { inject } from '@angular/core';
import { UsersService } from '../services/users.service'
import { User } from '../models/user'

export const userDetailedResolver: ResolveFn<User> = (route, state) => {
  const userService = inject(UsersService);

  console.log(route.paramMap);
  
  return userService.getUser(route.paramMap.get('name')!)
};