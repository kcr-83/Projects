import { Component, OnDestroy, OnInit } from '@angular/core';

@Component({
  selector: 'app-routing-guards',
  templateUrl: './routing-guards.component.html',
  styleUrls: ['./routing-guards.component.scss']
})
export class RoutingGuardsComponent implements OnDestroy {

  constructor() {}

  ngOnDestroy() {
    console.log('RoutinGuardComponent destroy')
  }
}
