import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import { UserRole } from 'src/app/services/user/user-roles.enum';
import { UserService } from 'src/app/services/user/user.service';

@Injectable({
  providedIn: 'root'
})
export class UserRoleGuard implements CanActivate {
  constructor(private readonly user: UserService) {}
  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
    const currUser = this.user.getRole()
    const allowedRoles = route.data['roles']
    const userIsAllowed = allowedRoles.includes(currUser)
    console.log('UserRoleGuard canActivate', currUser, allowedRoles)
    return userIsAllowed
  }

}
