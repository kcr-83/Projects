import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HighlightComponent } from '../app-components/highlight/highlight.component';
import { ConsoleComponent } from '../app-components/console/console.component';
import { MaterialModule } from '../material/material.module';



@NgModule({
  declarations: [
    HighlightComponent,
    ConsoleComponent
  ],
  imports: [
    CommonModule,
    MaterialModule
  ],
  exports: [
    HighlightComponent,
    ConsoleComponent
  ]
})
export class HelpersModule {}
