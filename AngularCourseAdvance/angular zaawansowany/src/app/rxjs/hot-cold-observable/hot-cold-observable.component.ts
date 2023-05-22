import { AfterViewInit, ChangeDetectionStrategy, ChangeDetectorRef, Component, ElementRef, Input, OnInit, ViewChild } from '@angular/core';
import { fromEvent, map, Observable, pluck, tap, reduce, scan, take, interval, Subscriber, Subject, share, startWith, BehaviorSubject, timer, connectable, of, filter } from 'rxjs';

@Component({
  selector: 'app-hot-cold-observable',
  templateUrl: './hot-cold-observable.component.html',
  styleUrls: ['./hot-cold-observable.component.scss'],
})
export class HotColdObservableComponent implements OnInit, AfterViewInit {
  @ViewChild('btn', { read: ElementRef }) eventButton!: ElementRef<HTMLButtonElement>;
  @Input() someData = 'wartość z inputa'

  protected nextOnInit = new Subject<string>()
  protected nextAfterViewInit = new Subject<string>()

  protected static1$: Observable<string> | undefined;
  protected static2$: Observable<string> | undefined;

  protected hotEvent$: Observable<string> | undefined;

  protected connect$: Observable<string> | undefined;
  protected staticHistory = ''

  // tap odpali się dla każdej nowej subskrypcji:(
  // setTimeout zapobiega expressionChangedAfterItHasBeenChecked err
  private coldNumber$ = new Observable((sub: Subscriber<string>) => {
    console.log('Nowa subskrypcja do cold')
    const random = Math.random().toFixed(2)
    interval(1000)
      .pipe(take(5)).subscribe({
        next: val => sub.next(`${val} (${random})`),
        error: err => sub.error(err),
        complete: () => sub.complete()
      })
  }).pipe(tap(val => setTimeout(() => this.staticHistory += val + ', ')))

  constructor(private cd: ChangeDetectorRef) {}

  ngOnInit() {
    // next idzie PRZED inicjalizacją widoku. Czyli async się jeszcze nie zapisał na nextOnInit$
    this.nextOnInit?.next('wartość z ngOnInit')
    this.nextAfterViewInit.subscribe(d => console.log('nextAfterViewInit$: ', d))
  }
  onConnectClick() {
    // każdy klik tworzy nową subskrypcję. Jak temu zapobiec?
    this.connect$ = this.coldNumber$.pipe(share())


  }
  onStaticSub1() {
    this.static1$ = this.coldNumber$
  }
  onStaticSub2() {
    this.static2$ = this.coldNumber$
  }

  ngAfterViewInit(): void {
    // ExpressionChangedAfterItHasBeenCheckedError
    // this.nextAfterViewInit.next(this.someData)

    // Promise.resolve().then(() => this.nextAfterViewInit.next(this.someData))
    // setTimeout(() => this.nextAfterViewInit.next(this.someData))
    timer(0).subscribe(() => this.nextAfterViewInit.next(this.someData))

    this.hotEvent$ = fromEvent<MouseEvent>(this.eventButton.nativeElement, 'click').pipe(
      map((ev) => 'Przycisk kliknięty')
    )
  }
}





