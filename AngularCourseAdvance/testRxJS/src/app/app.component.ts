import { filter, map } from 'rxjs';
import { Component, OnInit } from '@angular/core';
import { Event, NavigationEnd, NavigationStart, Router } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'testRxJS';
  isloading: boolean = true;

  constructor(private readonly router: Router) { }
  ngOnInit(): void {
    this.loading()
  }
  loading() {
    this.router.events.subscribe(
      event => {
        if (event instanceof NavigationStart) {
          this.isloading = true;
        }
        else if (event instanceof NavigationEnd || event instanceof NavigationEnd) {
          this.isloading = false;
        }
      }
    );
  }
}
                                                      