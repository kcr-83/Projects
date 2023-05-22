import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RxjsDashboardComponent } from './rxjs-dashboard/rxjs-dashboard.component';
import { RxjsRoutingModule } from './rxjs-routing.module';
import { MaterialModule } from '../material/material.module';
import { ObservablesComponent } from './observables/observables.component';
import { HelpersModule } from '../helpers/helpers.module';
import { HotColdObservableComponent } from './hot-cold-observable/hot-cold-observable.component';
import { ManyObservablesComponent } from './many-observables/many-observables.component';



@NgModule({
  declarations: [
    RxjsDashboardComponent,
    ObservablesComponent,
    HotColdObservableComponent,
    ManyObservablesComponent
  ],
  imports: [
    CommonModule,
    RxjsRoutingModule,
    MaterialModule,
    HelpersModule
  ]
})
export class RxjsModule {}
