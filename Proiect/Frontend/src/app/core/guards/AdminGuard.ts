import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { map } from 'rxjs/operators';
import { AuthentificationService } from '../services/authentification.service';

@Injectable({ providedIn: 'root' })
export class AdminGuard {
  constructor(private router: Router, private readonly authentificationService: AuthentificationService) { }

  async canActivate() {
    let ok: any;
    try {
      ok = await this.authentificationService.validare().pipe(map(data => Number(data))).toPromise();
      console.log(ok);
      if (ok === 0) {
        return true;
      } else {
        alert("Nu ai acces la această pagină!");
        this.router.navigate(['/register']);
        return false;
      }
    } catch (error) {
      alert("Nu ai acces la această pagină!");
      this.router.navigate(['/register']);
      return false;
    }
  }

  async isAdmin() {
    let ok: any;
    try {
      ok = await this.authentificationService.validare().pipe(map(data => Number(data))).toPromise();
      console.log(ok);
      if (ok === 0) {
        return true;
      } else {
        return false;
      }
    } catch (error) {
      return false;
    }
  }
}
