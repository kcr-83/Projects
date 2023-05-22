import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ComponentsRoutingModule } from './components-routing.module';
import { StandaloneComponent } from './standalone/standalone.component';
import { DynamicComponent } from './dynamic/dynamic.component';
import { ComponentsDashboardComponent } from './components-dashboard/components-dashboard.component';
import { MaterialModule } from '../material/material.module';
import { DecoratorsComponent } from './decorators/decorators.component';
import { MessageModule } from '../app-components/message/message/message.module';


@NgModule({
  declarations: [
    DynamicComponent,
    ComponentsDashboardComponent,
    DecoratorsComponent
  ],
  imports: [
    CommonModule,
    ComponentsRoutingModule,
    MaterialModule,
    MessageModule
  ]
})
export class ComponentsModule {}
