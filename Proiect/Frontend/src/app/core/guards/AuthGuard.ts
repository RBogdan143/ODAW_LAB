import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { AuthentificationService } from '../services/authentification.service';

@Injectable({ providedIn: 'root' })
export class AuthGuard implements CanActivate {
  constructor(private router: Router, private readonly authentificationService: AuthentificationService) { }

  canActivate() {
    this.authentificationService.validare().subscribe(
      data => { },
      erorr => {
        this.router.navigate(['/register']);
        alert("Nu mai eşti logat în cont!");
        return false;
      });
    
    return true;
  }
}
