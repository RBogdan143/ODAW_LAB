import { Injectable } from '@angular/core';
import { ApiService } from "./api.service";
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ProdusService {
  private readonly route = 'Client';
  constructor(private readonly apiService: ApiService) { }

  private produsSource = new BehaviorSubject<Produs | null>(null);
  currentProdus = this.produsSource.asObservable();

  changeProdus(produs: Produs) {
    this.produsSource.next(produs);
  }

  CreateCos(cos: any) {
    if (localStorage.getItem('token')) {
      const headers = {
        'Authorization': `Bearer ${localStorage.getItem('token')}`
      };
      return this.apiService.post(this.route + '/Adauga_Cos', cos, { headers: headers });
    }
    else {
      return this.apiService.post(this.route + '/Adauga_Cos', cos);
    }
  }

  AddToCos(IdProdus?: any, PromoCode?: any) {
    if (localStorage.getItem('token')) {
      const headers = {
        'Authorization': `Bearer ${localStorage.getItem('token')}`
      };
      if (IdProdus != null && PromoCode != null)
        return this.apiService.put(this.route + '/Modifica_Cos?PromoCode=${PromoCode}', IdProdus, { headers: headers });
      else if (IdProdus != null) {
        return this.apiService.put(this.route + '/Modifica_Cos', IdProdus, { headers: headers });
      } else if (PromoCode != null) {
        return this.apiService.put(this.route + '/Modifica_Cos?PromoCode=${PromoCode}', { headers: headers });
      } else return this.apiService.put(this.route + '/Modifica_Cos');
    }
    else {
      return this.apiService.put(this.route + '/Modifica_Cos');
    }
  }

  DelFromCos(IdProdus: any) {
    if (localStorage.getItem('token')) {
      const headers = {
        'Authorization': `Bearer ${localStorage.getItem('token')}`
      };
      return this.apiService.delete(this.route + '/Scoate_Produse?PromoCode=${IdProdus}', { headers: headers });
    }
    else {
      return this.apiService.delete(this.route + '/Scoate_Produse', IdProdus);
    }
  }

  Cumpara() {
    if (localStorage.getItem('token')) {
      const headers = {
        'Authorization': `Bearer ${localStorage.getItem('token')}`
      };
      return this.apiService.delete(this.route + '/Tranzactie', { headers: headers });
    }
    else {
      return this.apiService.delete(this.route + '/Tranzactie');
    }
  }
}

interface Produs {
  idProdus: string,
  produs: string,
  pretProdus: number,
  descriereProdus: string,
  imagineProdus: string,
  stoc: number
}
