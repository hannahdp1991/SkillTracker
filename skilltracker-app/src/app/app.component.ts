import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'skilltracker-app';

  constructor(private readonly router: Router) {
  }

  isActive(route: string): boolean {
    return this.router.url === `/` && route === "tracker" || this.router.url.includes(`/${route}`);
  }
}