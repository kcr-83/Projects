import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ChangeDetectionDashboardComponent } from './change-detection-dashboard/change-detection-dashboard.component';

const routes: Routes = [
  { path: '', component: ChangeDetectionDashboardComponent },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ChangeDetectionRoutingModule {}
