import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'exclaim'
})
export class ExclaimPipe implements PipeTransform {
  transform(value: string, times: number = 1): string {
    return value + '!'.repeat(times);
  }
}
