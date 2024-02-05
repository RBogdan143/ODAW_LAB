import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { PageEvent } from '@angular/material/paginator';
import { Router } from '@angular/router';
import { ProdusService } from '../core/services/produs.secvice'

@Component({
  selector: 'app-fetch-data',
  templateUrl: './fetch-data.component.html'
})

export class FetchDataComponent {
  public produse: any;

  numarDeElementePePagina = 9;
  paginaCurenta = 1;

  baseUrl = 'https://localhost:7146/api/Client/';

  constructor(private produsService: ProdusService, private router: Router, http: HttpClient) {
    http.get<ListaProduse[]>(this.baseUrl + 'Lista_Produse').subscribe(result => {
      this.produse = result;
      this.produse = this.produse.$values;
      if (this.produse != null && Array.isArray(this.produse))
        this.produse = this.produse
          .filter(item => item.stoc > 0) // Elimină produsele cu stocul 0
          .reduce((acc, item) => {
            // Dacă produsul nu există deja în array, îl adaugă
            if (Array.isArray(acc) && !acc.find(prod => prod.produs === item.produs)) {
              acc.push(item);
            }
            return acc;
          }, []);
      console.log(this.produse);
    }, error => console.error(error));
  }

  schimbaPagina(eveniment: PageEvent) {
    this.paginaCurenta = eveniment.pageIndex + 1;
  }

  vizitaProdus(produs: ListaProduse) {
    this.produsService.changeProdus(produs);
    this.router.navigate(['/produs']);
  }
}

interface ListaProduse {
  idProdus: string,
  produs: string,
  pretProdus: number,
  descriereProdus: string,
  imagineProdus: string,
  stoc: number
}
