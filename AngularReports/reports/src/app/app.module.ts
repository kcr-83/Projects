import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { WebdatarocksPivotModule } from 'ng-webdatarocks';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';


@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    WebdatarocksPivotModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
