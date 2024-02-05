import { Component } from '@angular/core';
import { AdminGuard } from './core/guards/AdminGuard';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html'
})
export class AppComponent {
  title = 'app';

  constructor(private readonly adminGuard: AdminGuard) { }

  IsAdmin = this.adminGuard.isAdmin();
}
