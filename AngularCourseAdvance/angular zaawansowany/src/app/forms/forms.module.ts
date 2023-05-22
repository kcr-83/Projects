import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { FormsRoutingModule } from './forms-routing.module';
import { FormsDashboardComponent } from './forms-dashboard/forms-dashboard.component';
import { ReactiveFormsModule } from '@angular/forms';
import { MaterialModule } from '../material/material.module';
import { FormBasicComponent } from './form-basic/form-basic.component';
import { CustomValidatorComponent } from './custom-validator/custom-validator.component';
import { CustomFormControlComponent } from './custom-form-control/custom-form-control.component';
import { UserRegisterComponent } from './user-register/user-register.component';


@NgModule({
  declarations: [
    FormsDashboardComponent,
    FormBasicComponent,
    CustomValidatorComponent,
    CustomFormControlComponent,
    UserRegisterComponent
  ],
  imports: [
    CommonModule,
    FormsRoutingModule,
    ReactiveFormsModule,
    MaterialModule
  ]
})
export class FormsModule {}
