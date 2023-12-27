import { Injectable } from '@angular/core';
import { ApiService } from "./api.service";

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  private readonly route = 'Users';
  constructor(private readonly apiService: ApiService) { }

  UpdateProfile(user: any) {
    if (localStorage.getItem('token')) {
      const headers = {
        'Authorization': `Bearer ${localStorage.getItem('token')}`
      };
      return this.apiService.put(this.route + '/UpdateProfile', user, { headers: headers });
    }
    else {
      return this.apiService.put(this.route + '/UpdateProfile', user);
    }
  }
}
