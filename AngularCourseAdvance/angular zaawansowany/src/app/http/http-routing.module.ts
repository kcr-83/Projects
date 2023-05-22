import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HttpDashboardComponent } from './http-dashboard/http-dashboard.component';

const routes: Routes = [
  { path: '', component: HttpDashboardComponent },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class HttpRoutingModule {}
