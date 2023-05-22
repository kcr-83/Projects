import { AfterViewInit, Component, ElementRef, OnInit, ViewChild, ViewContainerRef } from '@angular/core';
import { MessageComponent } from 'src/app/app-components/message/message.component';

@Component({
  selector: 'app-dynamic',
  templateUrl: './dynamic.component.html',
  styleUrls: ['./dynamic.component.scss']
})
export class DynamicComponent implements OnInit {
  private dashboardItems = [MessageComponent, MessageComponent, MessageComponent]
  constructor() {}

  @ViewChild('dynamicDashboard', { read: ViewContainerRef }) dashboardVCR!: ViewContainerRef;

  //eslint-disable-next-line
  ngOnInit(): void {
    // jeśli chcesz mieć dostęp do viewContainerRef dynamicDashboardu PRZED afterViewInit 
    // udekoruj dynamicDashboard custom dyrektywą z ViewContainerRef w konstruktorze
  }

  onCreateWidgets() {
    for (let i = 0; i < this.dashboardItems.length; i++) {
      const componentRef = this.dashboardVCR.createComponent<MessageComponent>(this.dashboardItems[i]);
      componentRef.instance.title = 'Widget ' + i
      componentRef.instance.sentencesCount = 3
      // componentRef.instance.action.subscribe
      console.log(componentRef.instance)
    }
  }
  onClearDashboard() {
    this.dashboardVCR.clear();
  }
}
