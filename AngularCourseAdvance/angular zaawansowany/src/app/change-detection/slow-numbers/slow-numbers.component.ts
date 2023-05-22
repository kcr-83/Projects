import { ChangeDetectionStrategy, ChangeDetectorRef, Component, DoCheck, Input, OnDestroy, OnInit } from '@angular/core';
import { Observable, Subscription, take } from 'rxjs';
import { NumbersService } from 'src/app/services/numbers/numbers.service';


@Component({
  selector: 'app-slow-numbers',
  templateUrl: './slow-numbers.component.html',
  styleUrls: ['./slow-numbers.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class SlowNumbersComponent implements OnInit, DoCheck, OnDestroy {

  @Input() licznik!: number | null
  @Input() licznik2!: number | null

  protected randomValues: number[] = []

  protected licznikInternal = 0;
  protected licznikZSerwisu!: number;
  protected licznikZSerwisu$!: Observable<number>;

  private readonly arraySize = 50
  private readonly fiboKiller = 30
  private readonly genericSub = new Subscription()
  private intervalHandler!: ReturnType<typeof setInterval>

  constructor(private readonly numbersService: NumbersService, private readonly cdRef: ChangeDetectorRef) {}

  ngOnInit(): void {
    this.cdRef.detach();
    for (let i = 0; i < this.arraySize; i++) {
      this.randomValues.push(Math.floor(Math.random() * this.fiboKiller));
    }

    this.licznikZSerwisu$ = this.numbersService.getNumbers();

    const sub = this.numbersService.getNumbers().subscribe(val => {
      console.log('slow numbers, service sub', val)
      this.licznikZSerwisu = val
    });
    this.genericSub.add(sub)

    // zobaczmy na doCheck co się stanie jak zmniejszymy timeout
    this.intervalHandler = setInterval(() => {
      this.licznikInternal++
      // console.log('slow numbers komponent, interval', this.licznikInternal)
    }, 10)
  }

  ngDoCheck() {
    console.log('[slowNumbers component] ngDoCheck')
  }

  verySlowCalculate(val: number) {
    console.log('Calculate ', val)
    return this.fibopago(val)
  }
  private fibopago(val: number): number {
    return val <= 2
      ? 1
      : this.fibopago(val - 1) + this.fibopago(val - 2);
  }
  protected onClick() {
    console.log('BTN KLIK')
  }
  ngOnDestroy() {
    this.genericSub.unsubscribe();
    clearInterval(this.intervalHandler)
  }
}
