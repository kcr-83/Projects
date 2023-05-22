import { Component, OnDestroy, OnInit } from '@angular/core';
import { interval, map, Subscription, timer, zip, take, Observable } from 'rxjs';


@Component({
  selector: 'app-vouting-timer',
  templateUrl: './vouting-timer.component.html',
  styleUrls: ['./vouting-timer.component.css']
})
export class VoutingTimerComponent implements OnInit, OnDestroy {

  sub = new Subscription();
  vote1 = timer(1000).pipe(map(() => 'za'));
  vote2 = timer(2000).pipe(map(() => 'przeciw'));
  vote3 = timer(3000).pipe(map(() => 'za'));
  obs!: Observable<number>;

  constructor() {

  }
  ngOnDestroy(): void {
    this.sub.unsubscribe()
  }

  ngOnInit(): void {
    var zip$ = zip(this.vote1, this.vote2, this.vote3) //forkJoin, combineLastest
      .pipe(map(wynik => 'wynik glosowania: ' + wynik.join(', ')))
      .subscribe(console.log)
    this.obs = interval(1000);
    var obs$ = this.obs.subscribe(console.log);
    this.sub.add(zip$);
    this.sub.add(obs$);
  }


}
