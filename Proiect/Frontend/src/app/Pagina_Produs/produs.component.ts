import { Component } from '@angular/core';
import { ProdusService } from '../core/services/produs.secvice';

@Component({
  selector: 'app-produs',
  templateUrl: './produs.component.html'
})

export class ProdusComponent {
  produs: any;

  constructor(private readonly produsService: ProdusService) { }

  ngOnInit() {
    this.produsService.currentProdus.subscribe(produs => this.produs = produs);
  }

  message: any;

  MakeCos(Id: any) {
    let cos = {
      IdProdus: Id
    };
    this.produsService.CreateCos(cos).subscribe(
      data => {
        this.message = "Produsul a fost adăugat în coş";
        localStorage.setItem('cos', "Exista");
      },
      error => {
        this.message = "Produsul nu a putut fi adăugat în coş";
        console.log(error);
      });
  }

  GrowCos(IdProdus: any) {
    if (IdProdus != null)
      this.produsService.AddToCos(IdProdus).subscribe(
        data => {
          this.message = "Produsul a fost adăugat în coş";
          localStorage.setItem('cos', "Exista");
        },
        error => {
          this.message = "Produsul nu a putut fi adăugat în coş";
          console.log(error);
        });
    else this.message = "Produsul nu a putut fi adăugat în coş";
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
