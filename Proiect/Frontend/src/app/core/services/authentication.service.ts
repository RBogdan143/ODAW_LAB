import { Injectable } from '@angular/core';
import {ApiService} from "./api.service";

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {
  private readonly route = 'Users';
  constructor(private readonly apiService: ApiService) { }

  register(user: any){
    return this.apiService.post(this.route + '/register', user);
  }

  login(user: any){
    return this.apiService.post(this.route + '/login', user);
  }
}
