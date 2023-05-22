import { Injectable } from '@angular/core';
import { tap, Observable, catchError, of, map } from 'rxjs';
import { JWT } from 'src/app/models/token.model';
import { ApiService } from '../api/api.service';
import { UserRole } from './user-roles.enum';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private _role: UserRole = UserRole.Admin
  private token: JWT | undefined
  private refreshToken: JWT | undefined
  constructor(private readonly api: ApiService) {}

  getRole() {
    return this._role
  }
  loadToken(expireTime: number) {
    return this.api.getToken(expireTime)
      .pipe(
        tap(data => {
          this.token = data.token
          this.refreshToken = data.refreshToken
        })
      )
  }

  getToken(): JWT | undefined {
    return this.token
  }
  getRefreshToken(): JWT | undefined {
    return this.refreshToken
  }
  refreshJWT() {
    return this.api.refreshToken(this.refreshToken as JWT)
      .pipe(
        tap(data => {
          this.token = data.token
          this.refreshToken = data.refreshToken
        })
      )
  }
  deleteToken() {
    this.token = undefined
  }
}
