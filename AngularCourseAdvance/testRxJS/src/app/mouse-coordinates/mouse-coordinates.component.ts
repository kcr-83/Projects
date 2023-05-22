import { Component, OnDestroy, OnInit } from '@angular/core';
import { map } from 'rxjs';
import { fromEvent } from 'rxjs/internal/observable/fromEvent';
import { Subscription } from 'rxjs/internal/Subscription';

@Component({
  selector: 'app-mouse-coordinates',
  templateUrl: './mouse-coordinates.component.html',
  styleUrls: ['./mouse-coordinates.component.css']
})
export class MouseCoordinatesComponent implements OnInit, OnDestroy {

  subscription!: Subscription;
  constructor() { }

  ngOnInit(): void {
    this.subscription =
      fromEvent(document, 'click')
        .subscribe(e => {
          console.log(e);
        });
        this.subscription = fromEvent<MouseEvent>(document, 'click')
        .pipe(map(ev => `${ev.clientX}/${ev.clientY}`))
        .subscribe(e => {
          console.log(e);
        });
  }
  ngOnDestroy() {
    this.subscription.unsubscribe();
  }
}
