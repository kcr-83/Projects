import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ChangeDetectionRoutingModule } from './change-detection-routing.module';
import { ChangeDetectionDefaultComponent } from './change-detection-default/change-detection-default.component';
import { ChangeDetectionOnpushComponent } from './change-detection-onpush/change-detection-onpush.component';
import { ChangeDetectionDashboardComponent } from './change-detection-dashboard/change-detection-dashboard.component';
import { MaterialModule } from '../material/material.module';
import { SlowNumbersComponent } from './slow-numbers/slow-numbers.component';
import { MultiplyPipe } from '../pipes/multiply.pipe';


@NgModule({
  declarations: [
    ChangeDetectionDefaultComponent,
    ChangeDetectionOnpushComponent,
    ChangeDetectionDashboardComponent,
    SlowNumbersComponent,
    MultiplyPipe
  ],
  imports: [
    CommonModule,
    ChangeDetectionRoutingModule,
    MaterialModule
  ]
})
export class ChangeDetectionModule {}
