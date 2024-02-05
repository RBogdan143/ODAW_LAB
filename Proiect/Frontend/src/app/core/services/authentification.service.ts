import { Injectable } from '@angular/core';
import {ApiService} from "./api.service";

@Injectable({
  providedIn: 'root'
})
export class AuthentificationService {
  private readonly route = 'Users';
  constructor(private readonly apiService: ApiService) { }

  register(user: any){
    return this.apiService.post(this.route + '/register', user);
  }

  login(user: any){
    return this.apiService.post(this.route + '/login', user);
  }

  validare() {
    if (localStorage.getItem('token')) {
      const headers = {
        'Authorization': `Bearer ${localStorage.getItem('token')}`
      };
      return this.apiService.get(this.route + '/Validare', { headers: headers });
    }
    else {
      return this.apiService.get(this.route + '/Validare');
    }
  }
}
