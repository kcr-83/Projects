import { Component, OnDestroy, OnInit } from '@angular/core';
import { Subscription, take } from 'rxjs';

@Component({
  selector: 'app-router-events',
  templateUrl: './router-events.component.html',
  styleUrls: ['./router-events.component.scss']
})
export class RouterEventsComponent implements OnDestroy {

  genericSub = new Subscription();

  constructor() {}


  onOutlet(data: any, name: string) {
    console.log(`Router outlet ${name} event`, data)
    const sub = data.action.subscribe((action: string) => console.log(action))
    console.log(data)
    this.genericSub.add(sub)
  }

  ngOnDestroy() {
    this.genericSub.unsubscribe()
  }
}
