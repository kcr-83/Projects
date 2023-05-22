import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HeaderComponent } from './app-components/header/header.component';
import { MaterialModule } from './material/material.module';
import { AppComponent } from './app-components/app/app.component';
import { HelpersModule } from './helpers/helpers.module';
import { TitleStrategy } from '@angular/router';
import { CustomTitleStrategy } from './helpers/custom-title-strategy';
import { DashboardComponent } from './app-components/dashboard/dashboard.component';
import { NotFoundComponent } from './app-components/not-found/not-found.component';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { LoggerInterceptor } from './interceptors/logger/logger.interceptor';
import { PiaskownicaComponent } from './piaskownica/piaskownica/piaskownica.component';
import { PiaskownicaModule } from './piaskownica/piaskownica.module';
import { MessageModule } from './app-components/message/message/message.module';
import { PurePipe } from './pipes/pure.pipe';
import { TokenInterceptor } from './interceptors/token/token.interceptor';
import { MatCardModule } from '@angular/material/card';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { MatToolbarModule } from '@angular/material/toolbar';

@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    DashboardComponent,
    NotFoundComponent,
    PurePipe,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    HttpClientModule,
    MaterialModule,
    HelpersModule,
    PiaskownicaModule,
    MessageModule,
  ],
  providers: [
    // {
    //   provide: TitleStrategy,
    //   useClass: CustomTitleStrategy
    // },
    // { provide: HTTP_INTERCEPTORS, useClass: LoggerInterceptor, multi: true },
    // { provide: HTTP_INTERCEPTORS, useClass: TokenInterceptor, multi: true },
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
