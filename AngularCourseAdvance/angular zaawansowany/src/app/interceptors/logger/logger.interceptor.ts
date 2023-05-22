import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { Observable, tap } from 'rxjs';

@Injectable()
export class LoggerInterceptor implements HttpInterceptor {

  JWT = 'sdfsdf23-sdffq345sdf-34t-5w34-5w34-5'
  constructor() {}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    console.log('Logger interceptor: ', request, next)
    return next.handle(request).pipe(tap(data => console.log('Logger interceptor response: ', data)))
  }
}
