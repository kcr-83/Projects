import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ContentProjectionRoutingModule } from './content-projection-routing.module';
import { ContentProjectionDashboardComponent } from './content-projection-dashboard/content-projection-dashboard.component';
import { ProjectionContainerComponent } from './projection-container/projection-container.component';
import { MessageModule } from '../app-components/message/message/message.module';
import { MaterialModule } from '../material/material.module';


@NgModule({
  declarations: [
    ContentProjectionDashboardComponent,
    ProjectionContainerComponent
  ],
  imports: [
    CommonModule,
    ContentProjectionRoutingModule,
    MaterialModule,
    MessageModule
  ]
})
export class ContentProjectionModule {}
