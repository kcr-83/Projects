import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { MouseCoordinatesComponent } from './mouse-coordinates/mouse-coordinates.component';
import { VoutingTimerComponent } from './vouting-timer/vouting-timer.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatRadioModule } from '@angular/material/radio';
import { MatButtonModule } from '@angular/material/button';
import { SearchBoxComponent } from './search-box/search-box.component';
import { MatInputModule } from '@angular/material/input';
import { LongLoadingComponent } from './long-loading/long-loading.component';
import { MatProgressBarModule } from '@angular/material/progress-bar';

@NgModule({
  declarations: [
    AppComponent,
    MouseCoordinatesComponent,
    VoutingTimerComponent,
    SearchBoxComponent,
    LongLoadingComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    MatRadioModule,
    MatButtonModule,
    MatInputModule,
    MatProgressBarModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
