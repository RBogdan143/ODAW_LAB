import { Injectable } from '@angular/core';
import { ApiService } from "./api.service";

@Injectable({
  providedIn: 'root'
})
export class AdminService {
  private readonly route = 'Users';
  constructor(private readonly apiService: ApiService) { }

  Update(username: any, role: any) {
    if (localStorage.getItem('token')) {
      const headers = {
        'Authorization': `Bearer ${localStorage.getItem('token')}`
      };
      return this.apiService.put(`${this.route}/AssignRole?username=${username}&role=${role}`, username, { headers: headers });
    }
    else {
      return this.apiService.put(this.route + '/AssignRole', username, role);
    }
  }

  Remove(username: any) {
    if (localStorage.getItem('token')) {
      const headers = {
        'Authorization': `Bearer ${localStorage.getItem('token')}`
      };
      return this.apiService.put(`${this.route}/RemoveRole?username=${username}`, username, { headers: headers });
    }
    else {
      return this.apiService.put(this.route + '/RemoveRole', username);
    }
  }

  GetUser(username: any) {
    if (localStorage.getItem('token')) {
      const headers = {
        'Authorization': `Bearer ${localStorage.getItem('token')}`
      };
      return this.apiService.get(`${this.route}/Find_User?username=${username}`, { headers: headers });
    }
    else {
      return this.apiService.put(this.route + '/Find_User', username);
    }
  }

  Delete(username: any) {
    if (localStorage.getItem('token')) {
      const headers = {
        'Authorization': `Bearer ${localStorage.getItem('token')}`
      };
      return this.apiService.delete(`${this.route}/Delete_User?username=${username}`, { headers: headers });
    }
    else {
      return this.apiService.put(this.route + '/Delete_User', username);
    }
  }
}
