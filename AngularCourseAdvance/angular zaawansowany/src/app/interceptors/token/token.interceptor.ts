import { Injectable } from '@angular/core';
import { HttpEvent, HttpInterceptor, HttpHandler, HttpRequest, HttpErrorResponse } from '@angular/common/http';
import { Observable, tap, catchError, Subject, BehaviorSubject, throwError, filter, finalize, switchMap, take, of, map } from 'rxjs';
import { JWT } from 'src/app/models/token.model';
import { UserService } from 'src/app/services/user/user.service';

@Injectable()
export class TokenInterceptor implements HttpInterceptor {
  whiteList = ['token', 'refreshToken']

  constructor(private readonly user: UserService) {}

  refreshTokenInProgress = false
  refreshToken = new Subject<JWT>()
  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    const requestWithToken = this.addTokenToRequest(request)

    console.log('Token interceptor: ', request, requestWithToken, next)

    return next.handle(requestWithToken).pipe(
      tap(data => console.log('Token interceptor response: ', data)),
      catchError((error: HttpErrorResponse) => {
        // tu obsługujemy wszystkie wyjątki/błędy - 500tki, 400tki etc

        // token wygasł, trzeba zrobić refresh
        if (error?.status === 401) {
          return this.handleTokenRefresh(requestWithToken, next)
        } else {
          return throwError(() => new Error(error.message));
        }
      })
    );
  }
  private addTokenToRequest<T>(request: HttpRequest<T>) {
    if (this.whiteList.some(el => el === request.url)) {
      return request
    }

    const token = this.user.getToken()
    let headers = request.headers
      .set('Authorization', `Bearer ${token}`);
    // klon, nie można ponownie użyć zmienionego HttpRequest
    return request.clone<T>({ headers });
  }
  private handleTokenRefresh<T>(request: HttpRequest<T>, next: HttpHandler) {
    if (this.refreshTokenInProgress) {
      // wcześniej poszło już zapytanie do refresh token endpoint, czekamy na nowy token
      return this.refreshToken.pipe(
        // filter((result) => result),
        take(1),
        switchMap(() => next.handle(this.addTokenToRequest(request)))
      );
    } else {
      // pierwszy request któremu wygasł token - zawieszamy je, wysyłamy żądanie o nowy token do refresh token endpoint
      this.refreshTokenInProgress = true;
      // this.refreshToken.next(null);

      return this.postTokenRefresh().pipe(
        tap(token => this.refreshToken.next(token)),
        switchMap(token => next.handle(this.addTokenToRequest(request))),
        finalize(() => this.refreshTokenInProgress = false)
      );
    }
  }
  private postTokenRefresh() {
    return this.user.refreshJWT().pipe(map(data => data.refreshToken))
  }
}