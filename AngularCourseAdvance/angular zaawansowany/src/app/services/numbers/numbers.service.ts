import { Injectable } from '@angular/core';
import { interval, Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class NumbersService {

  private secondFromStart = new Subject();
  constructor() {
    interval(1000).subscribe(seconds => this.secondFromStart.next(seconds));
  }
  getNumbers() {
    return interval(1000)
  }
  getSecondFromStart() {
    return this.secondFromStart.asObservable();
  }
}
