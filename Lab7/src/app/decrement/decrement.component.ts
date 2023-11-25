//Temă 1
import { Component } from '@angular/core';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';

@Component({
  selector: 'app-decrement',
  standalone: true,
  imports: [MatCardModule, MatButtonModule],
  templateUrl: './decrement.component.html',
  styleUrls: ['./decrement.component.scss']
})
export class DecrementComponent {
  public title: string = "Decrement Component";
  counter: number = 0;

  decrementCounter() {
    this.counter--;
  }
}
