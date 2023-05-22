import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { RoutingExamplesRoutingModule } from './routing-examples-routing.module';
import { RoutingDashboardComponent } from './routing-dashboard/routing-dashboard.component';
import { MaterialModule } from '../material/material.module';
import { RoutingParamsComponent } from './routing-params/routing-params.component';
import { RoutingSameRouteComponent } from './routing-same-route/routing-same-route.component';
import { RefreshViewComponent } from './refresh-view/refresh-view.component';
import { RouterEventsComponent } from './router-events/router-events.component';


@NgModule({
  declarations: [
    RoutingDashboardComponent,
    RoutingParamsComponent,
    RoutingSameRouteComponent,
    RefreshViewComponent,
    RouterEventsComponent,
  ],
  imports: [
    CommonModule,
    RoutingExamplesRoutingModule,
    MaterialModule
  ]
})
export class RoutingExamplesModule {}
