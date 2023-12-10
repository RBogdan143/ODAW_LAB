import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterOutlet } from '@angular/router';
import {DemoComponent} from "./demo/demo.component";
//Temă 4
import { DecrementComponent } from "./decrement/decrement.component";
import { DemoRequestComponent } from "./demo-request/demo-request.component";
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule, RouterOutlet, DemoComponent, DecrementComponent, DemoRequestComponent, FormsModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent {
  title = 'lab7';
}
