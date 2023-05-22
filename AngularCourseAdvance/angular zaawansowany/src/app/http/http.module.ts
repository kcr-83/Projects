import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { HttpRoutingModule } from './http-routing.module';
import { HttpDashboardComponent } from './http-dashboard/http-dashboard.component';
import { MaterialModule } from '../material/material.module';


@NgModule({
  declarations: [
    HttpDashboardComponent
  ],
  imports: [
    CommonModule,
    HttpRoutingModule,
    MaterialModule
  ]
})
export class HttpModule {}
