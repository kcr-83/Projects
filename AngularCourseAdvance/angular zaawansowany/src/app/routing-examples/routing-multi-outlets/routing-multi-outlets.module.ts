import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { RoutingMultiOutletsRoutingModule } from './routing-multi-outlets-routing.module';
import { MultiOutletComponent } from '../multi-outlet/multi-outlet.component';
import { MaterialModule } from 'src/app/material/material.module';


@NgModule({
  declarations: [MultiOutletComponent],
  imports: [
    CommonModule,
    RoutingMultiOutletsRoutingModule,
    MaterialModule
  ]
})
export class RoutingMultiOutletsModule {}
