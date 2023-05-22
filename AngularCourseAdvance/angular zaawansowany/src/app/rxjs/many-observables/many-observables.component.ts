import { Component, OnInit } from '@angular/core';
import { Subject, interval, share, scan, Observable, merge, zip, raceWith, map, zipWith, switchMap, combineLatestWith, tap, forkJoin, pipe, race, combineLatest, from, Subscription } from 'rxjs';

@Component({
  selector: 'app-many-observables',
  templateUrl: './many-observables.component.html',
  styleUrls: ['./many-observables.component.scss']
})
export class ManyObservablesComponent implements OnInit {
  protected interval900$ = interval(900).pipe(share())
  protected interval1600$ = interval(1600).pipe(share())
  protected interval3000$ = interval(3000).pipe(share())

  private btn1 = new Subject<number>()
  private btn1ClickCount = 0
  private btn2 = new Subject<number>()
  private btn3 = new Subject<number>()

  protected raceWith$!: Observable<string | number>
  protected zipWith$!: Observable<(string | number)[]>
  protected combineLatestWith$!: Observable<(string | number)[]>
  protected forkJoin$!: Observable<(string | number)[]>
  protected merge$!: Observable<(string | number)>
  protected switchMap$!: Observable<(string | number)>

  protected btn1$!: Observable<string | number>
  protected btn2$!: Observable<string | number>
  protected btn3$!: Observable<string | number>

  private genericSub = new Subscription()
  constructor() {}

  ngOnInit(): void {
    // custom pipe z parametrem
    const sumujIWyswietl = (name: string) => pipe(
      scan((acc: number, val: number) => acc += val),
      map(v => name + '-' + v)
    )
    this.btn1$ = this.btn1.pipe(sumujIWyswietl('btn1'))
    this.btn2$ = this.btn2.pipe(sumujIWyswietl('btn2'))
    this.btn3$ = this.btn3.pipe(sumujIWyswietl('btn3'))

    // this.raceWith$ = this.interval3000$.pipe(raceWith(this.btn1$))
    this.raceWith$ = race(this.interval3000$, this.btn1$)

    this.zipWith$ = zip(this.interval900$, this.interval1600$, this.interval3000$)
    // this.zipWith$ = this.interval900$.pipe(zipWith(this.interval1600$, this.interval3000$))

    // this.combineLatestWith$ = this.btn1$.pipe(combineLatestWith(this.btn2$), tap(console.log))
    this.combineLatestWith$ = combineLatest([this.btn1$, this.btn2$]).pipe(tap(console.log))
    this.forkJoin$ = forkJoin([this.btn1$, this.btn2$])

    this.merge$ = merge(this.btn1$, this.btn2$, this.btn3$)
    const mergeSub = this.merge$.subscribe()
    this.genericSub.add(mergeSub)

    this.switchMap$ = this.btn1$.pipe(switchMap(val => interval(1000).pipe(map(n => val + ' / ' + n))))
  }

  onEmitClick(num: 1 | 2 | 3) {
    // this.btn1
    // this['btn1']
    this[`btn${num}`].next(1)
  }
  // onEmitClick1() {
  //   this.btn1ClickCount++
  //   this.btn1.next(`btn1 - ${this.btn1ClickCount}`)
  // }
  // onEmitClick2() {
  //   this.btn2.next(1)
  // }
  // onEmitClick3() {
  //   this.btn3.next(1)
  // }
  onCompleteClick(num: 1 | 2 | 3) {
    this[`btn${num}`].complete();
  }
  ngOnDestroy() {
    this.genericSub.unsubscribe()
  }
}
