import { Component, OnInit } from '@angular/core';
import { NavigationCancel, NavigationEnd, NavigationStart, Router } from '@angular/router';
import { interval, tap } from 'rxjs';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  title = 'Angular zaawansowany - materiały szkoleniowe'

  protected w1 = 0;
  protected w2 = 0;
  protected isLoading = false;

  constructor(private readonly router: Router) {}

  ngOnInit(): void {
    this.handleLoader()
  }
  private handleLoader() {
    // this.spinner$ = this.router.events.pipe(
    //   filter(event => event instanceof NavigationEnd || event instanceof NavigationCancel || event instanceof NavigationStart),
    //   debounceTime(100),
    //   distinctUntilChanged(),
    //   map((e) => e instanceof NavigationStart))
    // )

    this.router.events.subscribe(event => {
      if (event instanceof NavigationStart) {
        this.isLoading = true
      }
      if (event instanceof NavigationEnd || event instanceof NavigationCancel) {
        this.isLoading = false
      }
    })
  }
}
