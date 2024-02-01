import { Injectable } from '@angular/core';
import { ApiService } from "./api.service";

@Injectable({
  providedIn: 'root'
})
export class AdminService {
  private readonly route1 = 'Users';

  private readonly route2 = 'Admin';
  constructor(private readonly apiService: ApiService) { }

  Update(username: any, role: any) {
    if (localStorage.getItem('token')) {
      const headers = {
        'Authorization': `Bearer ${localStorage.getItem('token')}`
      };
      return this.apiService.put(`${this.route1}/AssignRole?username=${username}&role=${role}`, username, { headers: headers });
    }
    else {
      return this.apiService.put(this.route1 + '/AssignRole', username, role);
    }
  }

  Remove(username: any) {
    if (localStorage.getItem('token')) {
      const headers = {
        'Authorization': `Bearer ${localStorage.getItem('token')}`
      };
      return this.apiService.put(`${this.route1}/RemoveRole?username=${username}`, username, { headers: headers });
    }
    else {
      return this.apiService.put(this.route1 + '/RemoveRole', username);
    }
  }

  GetUser(username: any) {
    if (localStorage.getItem('token')) {
      const headers = {
        'Authorization': `Bearer ${localStorage.getItem('token')}`
      };
      return this.apiService.get(`${this.route1}/Find_User?username=${username}`, { headers: headers });
    }
    else {
      return this.apiService.get(this.route1 + '/Find_User', username);
    }
  }

  Delete(username: any) {
    if (localStorage.getItem('token')) {
      const headers = {
        'Authorization': `Bearer ${localStorage.getItem('token')}`
      };
      return this.apiService.delete(`${this.route1}/Delete_User?username=${username}`, { headers: headers });
    }
    else {
      return this.apiService.delete(this.route1 + '/Delete_User', username);
    }
  }

  CreateProd(produs: any, stoc: any) {
    if (localStorage.getItem('token')) {
      const headers = {
        'Authorization': `Bearer ${localStorage.getItem('token')}`
      };
      return this.apiService.post(`${this.route2}/Adauga_Produs?produs=${produs}&stoc.Nr_Produse=${stoc.Nr_Produse}`, produs, { headers: headers });
    }
    else {
      return this.apiService.post(this.route2 + '/Adauga_Produs', produs);
    }
  }

  AddStoc(stoc: any, IdProdus: any) {
    if (localStorage.getItem('token')) {
      const headers = {
        'Authorization': `Bearer ${localStorage.getItem('token')}`
      };
      return this.apiService.post(`${this.route2}/Adauga_Stoc_Partener?IdProdus=${IdProdus}`, stoc, { headers: headers });
    }
    else {
      return this.apiService.post(this.route2 + '/Adauga_Stoc_Partener', stoc);
    }
  }

  CreateDisc(discount: any) {
    if (localStorage.getItem('token')) {
      const headers = {
        'Authorization': `Bearer ${localStorage.getItem('token')}`
      };
      return this.apiService.post(`${this.route2}/Adauga_Discount`, discount, { headers: headers });
    }
    else {
      return this.apiService.post(this.route2 + '/Adauga_Discount', discount);
    }
  }

  UpdateProd(stocProdus: any) {
    if (localStorage.getItem('token')) {
      const headers = {
        'Authorization': `Bearer ${localStorage.getItem('token')}`
      };
      return this.apiService.put(`${this.route2}/Update_Produs`, stocProdus, { headers: headers });
    }
    else {
      return this.apiService.put(this.route2 + '/Update_Produs', stocProdus);
    }
  }

  UpdateStoc(IdStoc: any, stoc: any) {
    if (localStorage.getItem('token')) {
      const headers = {
        'Authorization': `Bearer ${localStorage.getItem('token')}`
      };
      return this.apiService.put(`${this.route2}/Update_Stoc_Partener?IdStoc=${IdStoc}&stoc=${stoc}`, stoc, { headers: headers });
    }
    else {
      return this.apiService.put(this.route2 + '/Update_Stoc_Partener', IdStoc);
    }
  }

  DelProd(idprodus: any) {
    if (localStorage.getItem('token')) {
      const headers = {
        'Authorization': `Bearer ${localStorage.getItem('token')}`
      };
      return this.apiService.delete(`${this.route2}/Delete_Produs?idprodus=${idprodus}`, { headers: headers });
    }
    else {
      return this.apiService.delete(this.route2 + '/Delete_Produs', idprodus);
    }
  }

  DelStoc(idstoc: any) {
    if (localStorage.getItem('token')) {
      const headers = {
        'Authorization': `Bearer ${localStorage.getItem('token')}`
      };
      return this.apiService.delete(`${this.route2}/Delete_Stoc_Partener?idstoc=${idstoc}`, { headers: headers });
    }
    else {
      return this.apiService.delete(this.route2 + '/Delete_Stoc_Partener', idstoc);
    }
  }

  DelDisc(code: any) {
    if (localStorage.getItem('token')) {
      const headers = {
        'Authorization': `Bearer ${localStorage.getItem('token')}`
      };
      return this.apiService.delete(`${this.route2}/Delete_Discount?code=${code}`, { headers: headers });
    }
    else {
      return this.apiService.delete(this.route2 + '/Delete_Discount', code);
    }
  }

  GetProdStoc() {
    if (localStorage.getItem('token')) {
      const headers = {
        'Authorization': `Bearer ${localStorage.getItem('token')}`
      };
      return this.apiService.get(`${this.route2}/Produse+Stoc`, { headers: headers });
    }
    else {
      return this.apiService.get(this.route2 + '/Produse+Stoc');
    }
  }

  GetDisc() {
    if (localStorage.getItem('token')) {
      const headers = {
        'Authorization': `Bearer ${localStorage.getItem('token')}`
      };
      return this.apiService.get(`${this.route2}/Discount`, { headers: headers });
    }
    else {
      return this.apiService.get(this.route2 + '/Discount');
    }
  }
}
