import { AfterViewInit, Component, ElementRef, HostBinding, Input, OnChanges, OnDestroy, OnInit, SimpleChanges, ViewChild } from '@angular/core';
import { NavigationCancel, Router } from '@angular/router';
import { fromEvent, map, scan, switchMap, auditTime, throttleTime, sampleTime, pluck, pipe, Subject, Subscription, debounceTime, tap, timer, zip, filter, interval, forkJoin, Observable } from 'rxjs';

@Component({
  selector: 'app-piaskownica',
  templateUrl: './piaskownica.component.html',
  styleUrls: ['./piaskownica.component.scss']
})
export class PiaskownicaComponent implements OnInit, AfterViewInit, OnChanges, OnDestroy {

  @Input()
  private _wartosc1 = 0;
  public get wartosc1() {
    return this._wartosc1;
  }
  public set wartosc1(value) {
    this._wartosc1 = value;
    // this.wyliczSumaInputow()
  }
  @Input()
  private _wartosc2 = 0;
  public get wartosc2() {
    return this._wartosc2;
  }
  public set wartosc2(value) {
    this._wartosc2 = value;
    // this.wyliczSumaInputow()
  }
  @Input() wartosc3 = 0

  @ViewChild('searchBox') searchBox: ElementRef<HTMLInputElement> | undefined;
  // const searchData$ = fromEvent(this.searchBox.nativeElement, 'input')

  protected formattedHistory$!: Observable<string>

  private genericSub = new Subscription();
  private sumaInputow!: number

  constructor(private readonly router: Router) {}

  ngOnChanges(changes: SimpleChanges): void {
    console.log(changes)
    if (changes['wartosc1'] || changes['wartosc2'] || changes['wartosc3']) {
      this.sumaInputow = this.wartosc1 + this.wartosc2 + this.wartosc3;
    }
  }

  ngOnInit(): void {
    // this.zadanie1()
    this.zadanie2()
    // this.zadanie3()
    // this.router.events.subscribe(event => {
    //   if (event instanceof NavigationCancel) { 

    // }})
  }
  ngAfterViewInit() {
    this.zadanie4()
  }

  zadanie1() {
    // koordynaty myszy
    const sub = fromEvent<MouseEvent>(document, 'click')
      .pipe(
        map(ev => `${ev.clientX} / ${ev.clientY}`)
      )
      .subscribe(console.log)
    this.genericSub.add(sub)
  }
  zadanie2() {
    // koordynaty myszy - wersja co 1s
    // const sub = fromEvent<MouseEvent>(document, 'mousemove')
    //   .pipe(
    //     sampleTime(1000),
    //     // switchMap(ev => interval(1000).pipe(map(() => ev))), // powtarzaj ostatnią wartość co 1s
    //     map(ev => `${ev.clientX} / ${ev.clientY}`),
    //   )
    //   .subscribe(console.log)

    // wersja cisza od 2s i x>500
    // const sub = fromEvent<MouseEvent>(document, 'mousemove')
    //   .pipe(
    //     debounceTime(2000),
    //     filter(ev => ev.clientX > 500),
    //     map(ev => `${ev.clientX} / ${ev.clientY}`),
    //   )
    //   .subscribe(console.log)
    // this.genericSub.add(sub)

    // wersja "po co mi pipes"
    // let timeout!: ReturnType<typeof setTimeout>;
    // const sub = fromEvent<MouseEvent>(document, 'mousemove')
    //   .subscribe(ev => {
    //     if (ev.clientX <= 500) {
    //       return
    //     }
    //     if (timeout) {
    //       clearTimeout(timeout)

    //     }
    //     timeout = setTimeout(() => {
    //       const coords = `${ev.clientX} / ${ev.clientY}`
    //       console.log(coords)
    //     }, 2000)

    //   })
    // this.genericSub.add(sub)

  }
  zadanie3() {
    // głosowanie
    const vote1 = timer(1000).pipe(map(() => 'za'));
    const vote2 = timer(3000).pipe(map(() => 'przeciw'));
    const vote3 = timer(2000).pipe(map(() => 'za'));
    const sub = forkJoin([vote1, vote2, vote3]) // zip, combineLatest
      // wyniki: [vote1, vote2, vote3]
      .pipe(map(wyniki => 'Wyniki głosowania: ' + wyniki.join(', ')))
      .subscribe(console.log)
    this.genericSub.add(sub)
  }

  zadanie4() {
    if (!this.searchBox) { return }

    type SearchData = { history: string[], current: string }

    const searchData$ = fromEvent(this.searchBox.nativeElement, 'input')
      .pipe(
        debounceTime(300),
        map(ev => (ev.target as HTMLInputElement).value),
        filter(val => val !== ''),
        // filter(val => !!val),
        // filter(Boolean),
        scan((acc: SearchData, val: string) => ({
          history: [...acc.history, val],
          current: val
        }), { history: [], current: '' }),
      )
    const sub = searchData$.subscribe(console.log)
    this.genericSub.add(sub)
    // gdybym potrzebował historii niezależnie od emisji ostatniej wartości
    this.formattedHistory$ = searchData$.pipe(map(data => data.history.join(', ')))
  }

  dzien1() {
    // problem pluck-a
    // EDIT
    // gdybym bardzo chciał pluck-a
    // const userName: keyof User = 'fname' // keyofUser = 'fname' | 'age'

    // type User = { fname: string, age: number }
    // const sub = new Subject<User>()
    // sub.pipe(pluck(userName)).subscribe
    // sub.pipe(map(user => user.fname)).subscribe
  }
  dzien2() {
    // type jakasKolekcjaPocztowek = {
    //   [nazwaKraju: string]: string | string[]
    //   // poczt1: string
    //   // poczt2: string
    //   // pocztowkiWakacje: string[]
    // }
    // const pocztowki: jakasKolekcjaPocztowek = {
    //   grecja: 'pocz',
    //   polska: ['pocz', 'pocz2'],
    //   // wlochy: 12
    // }

    // const pocztowki2: Record<string, string | string[]> = {
    //   grecja: 'pocz',
    //   polska: ['pocz', 'pocz2'],
    //   // wlochy: 12
    // }
  }
  ngOnDestroy() {
    this.genericSub.unsubscribe()
  }
}
