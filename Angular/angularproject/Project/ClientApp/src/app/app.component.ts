import { Component, ElementRef, AfterViewInit } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html'
})

export class AppComponent {
  title = 'app';

  finish = false;
  content = false;

  constructor() { }

  ngOnInit() {
    this.finish = true;
    setTimeout(() => {
      this.content = true;
    }, 1800);
  }
}
