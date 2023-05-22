import { ChangeDetectionStrategy, Component, DoCheck, HostListener, OnDestroy, OnInit } from '@angular/core';
import { interval, Observable, Subscription, take } from 'rxjs';

@Component({
  selector: 'app-change-detection-dashboard',
  templateUrl: './change-detection-dashboard.component.html',
  styleUrls: ['./change-detection-dashboard.component.scss'],
})
export class ChangeDetectionDashboardComponent implements OnInit, DoCheck, OnDestroy {

  protected mode: 'default' | 'onpush' = 'default';
  protected licznik$!: Observable<number>;


  private genericSub = new Subscription()
  constructor() {}

  // pognębimy trochę ChangeDetection
  @HostListener('mousemove', ['$event'])
  onMouseMove(e: MouseEvent) {
    // console.log(e);
  }

  ngOnInit(): void {
    this.licznik$ = interval(1000)
  }
  ngDoCheck() {
    console.count('[CHANGE DETECTION DASHBOARD] ngDoCheck')
  }
  protected toggleMode(mode: 'default' | 'onpush') {
    this.mode = mode
  }
  ngOnDestroy() {
    this.genericSub.unsubscribe();
  }

}
