import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { RoutingGuardsRoutingModule } from './routing-guards-routing.module';
import { RoutingGuardsComponent } from './routing-guards.component';
import { MaterialModule } from 'src/app/material/material.module';


@NgModule({
  declarations: [
    RoutingGuardsComponent
  ],
  imports: [
    CommonModule,
    RoutingGuardsRoutingModule,
    MaterialModule
  ]
})
export class RoutingGuardsModule {}
