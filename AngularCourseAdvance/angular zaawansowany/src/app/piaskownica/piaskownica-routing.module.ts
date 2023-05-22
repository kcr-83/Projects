import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { PiaskownicaComponent } from './piaskownica/piaskownica.component';

const routes: Routes = [
  { path: '', component: PiaskownicaComponent },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class PiaskownicaRoutingModule {}
