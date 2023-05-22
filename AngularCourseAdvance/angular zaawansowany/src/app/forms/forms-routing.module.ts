import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CustomFormControlComponent } from './custom-form-control/custom-form-control.component';
import { CustomValidatorComponent } from './custom-validator/custom-validator.component';
import { FormBasicComponent } from './form-basic/form-basic.component';
import { FormsDashboardComponent } from './forms-dashboard/forms-dashboard.component';

const routes: Routes = [
  {
    path: '', component: FormsDashboardComponent,
    children: [
      { path: 'basic', component: FormBasicComponent },
      { path: 'validators', component: CustomValidatorComponent },
      { path: 'custom-control', component: CustomFormControlComponent },
    ]
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class FormsRoutingModule {}
