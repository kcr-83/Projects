import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ContentProjectionDashboardComponent } from './content-projection-dashboard/content-projection-dashboard.component';

const routes: Routes = [
  { path: '', component: ContentProjectionDashboardComponent },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ContentProjectionRoutingModule {}
