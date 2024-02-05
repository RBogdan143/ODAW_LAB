import {Directive, Input, OnInit} from '@angular/core';

@Directive({
  selector: '[appImagePreloader]',
  host: {
    '[attr.src]': 'finalImage'
  }
})
export class ImagePreloaderDirective implements OnInit {
  @Input('appImagePreloader') targetSource: string = "";
  defaultImage: string = 'https://www.creare-site-web.com/wp-content/uploads/2022/08/ce-inseamna-eroare-404-900x400.jpg';
  finalImage: any;
  downloadingImage: any;

  constructor() {
  }

  ngOnInit(): void {
    this.finalImage = this.defaultImage;
    this.downloadingImage = new Image();
    this.downloadingImage.src = this.targetSource;

    this.downloadingImage.onload = () => {
      this.finalImage = this.targetSource;
    }
    this.downloadingImage.onerror = (e: any) => console.error(e);
  }
}
