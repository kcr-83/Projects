import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ComponentsDashboardComponent } from './components-dashboard/components-dashboard.component';
import { DecoratorsComponent } from './decorators/decorators.component';
import { DynamicComponent } from './dynamic/dynamic.component';
import { StandaloneComponent } from './standalone/standalone.component';

const routes: Routes = [
  {
    path: '', component: ComponentsDashboardComponent,
    children: [
      { path: 'standalone', component: StandaloneComponent },
      { path: 'dynamic', component: DynamicComponent },
      { path: 'decorators', component: DecoratorsComponent },
    ]
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ComponentsRoutingModule {}
