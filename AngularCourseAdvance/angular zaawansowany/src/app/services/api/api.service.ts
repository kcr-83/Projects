import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { JWT } from 'src/app/models/token.model';

@Injectable({
  providedIn: 'root'
})
export class ApiService {
  readonly apiUrl = 'http://localhost:3000'
  constructor(private readonly http: HttpClient) {}

  getToken(expireTime?: number) {
    const url = this.apiUrl + '/token'
    const params = expireTime
      ? { exp: expireTime }
      : {}
    return this.http.post<{ token: JWT, refreshToken: JWT }>(url, params)
  }
  refreshToken(refreshToken: JWT) {
    const params = { refreshToken }
    const url = this.apiUrl + '/refreshToken'
    return this.http.post<{ token: JWT, refreshToken: JWT }>(url, params)
  }
  getProtected(number: 1 | 2 | 3): Observable<JWT> {
    const url = this.apiUrl + '/protected/' + number
    return this.http.get<JWT>(url)
  }

}
