import { Component, EventEmitter, OnDestroy, OnInit, Output } from '@angular/core';
import { AsyncSubject, BehaviorSubject, finalize, switchMap, tap, interval, noop, Observable, ReplaySubject, Subject, Subscriber, take, delay, timer, Subscription } from 'rxjs';

const EMIT_COUNT = 10

@Component({
  selector: 'app-observables',
  templateUrl: './observables.component.html',
  styleUrls: ['./observables.component.scss']
})
export class ObservablesComponent implements OnInit, OnDestroy {
  // Subject zamiast EventEmitter - taka ciekawostka:)
  @Output() event = new EventEmitter<string>()
  // @Output() event = new Subject()


  emitter = (sub: Subscriber<string>) => {
    const date = new Date().toLocaleTimeString();
    interval(1000).pipe(take(EMIT_COUNT)).subscribe({
      next: (n: number) => sub.next(`${date} - ${n}`),
      error: noop,
      complete: () => sub.complete()
    })
  }

  protected observable = new Observable(this.emitter);
  protected subject = new Subject<string>();
  // protected behaviorSubject = new BehaviorSubject<string>(null);
  protected behaviorSubject = new BehaviorSubject('wartość domyślna');
  // dwie ostatnie wartości z max ostatnich 4s
  // protected replaySubject = new ReplaySubject<string>(1);
  protected replaySubject = new ReplaySubject<string>(2, 4000);
  protected asyncSubject = new AsyncSubject<string>();
  protected lateRs = ''

  protected showLateSubs = false;
  protected showOnCompleteSubs = false;

  private replaySub!: Subscription

  private genericSub = new Subscription()

  constructor() {}

  ngOnInit(): void {

    const date = new Date().toLocaleTimeString();
    interval(1000).pipe(
      delay(2000),
      take(EMIT_COUNT),
      // finalize(() => {
      //   this.subject.complete();
      //   this.behaviorSubject.complete();
      //   this.replaySubject.complete();
      //   this.asyncSubject.complete();
      //   this.showOnCompleteSubs = true
      // })
    )
      .subscribe((n) => {
        const valueToEmit = `${date} - ${n}`

        this.subject.next(valueToEmit)
        this.behaviorSubject.next(valueToEmit)
        this.replaySubject.next(valueToEmit)
        this.asyncSubject.next(valueToEmit)

        // this.event.emit(valueToEmit)
        this.event.next(valueToEmit) //EventEmitter 😲

        if (n === EMIT_COUNT - 1) {
          this.subject.complete();
          this.behaviorSubject.complete();
          this.replaySubject.complete();
          this.asyncSubject.complete();
          this.showOnCompleteSubs = true
        }
      })
    // 😠 brak unsub-a, do dodania switchMapa
    timer(7000).subscribe(() => {
      this.replaySubject.subscribe(d => {
        this.showLateSubs = true
        console.log('Sub do ReplaySubject(2, 4s) po 5s: ', d)
        this.lateRs = d
      })
    })

    // poprawnie, bez sub-a w sub-ie:
    // const sub = timer(7000)
    //   .pipe(
    //     switchMap(() => this.replaySubject),
    //     tap((d) => {
    //       this.showLateSubs = true
    //       console.log('Sub do ReplaySubject(2, 4s) po 5s: ', d)
    //     }))
    //   .subscribe(d => {
    //     this.lateRs = d
    //   })
    // this.genericSub.add(sub)


    const a = this.subUsingForEach();
  }

  subUsingForEach() {
    console.log('[START] Emisja behaviorSubject z .forEach')
    const a = new Array(5, 2)
    // this.behaviorSubject.subscribe({
    //   next: (val) => console.log(`Emisja behaviorSubject z .forEach: `, val),
    //   error: (err) => {},
    //   complete: () => { console.log('[KONIEC] Emisja behaviorSubject z .forEach') }
    // }
    // )
    // await this.behaviorSubject.forEach(
    //   val => console.log(`Emisja behaviorSubject z .forEach: `, val)
    // )
    console.log('[KONIEC] Emisja behaviorSubject z .forEach')


  }

  protected onCompleteAsyncSubject() {
    this.asyncSubject.complete()
  }
  ngOnDestroy(): void {
    this.genericSub.unsubscribe()
    this.replaySub?.unsubscribe();
  }
}
