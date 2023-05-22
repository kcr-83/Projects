import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, CanDeactivate, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import { MessageComponent } from 'src/app/app-components/message/message.component';

@Injectable({
  providedIn: 'root'
})
export class DateGuard implements CanActivate, CanDeactivate<MessageComponent> {
  canDeactivate(component: MessageComponent, currentRoute: ActivatedRouteSnapshot, currentState: RouterStateSnapshot, nextState?: RouterStateSnapshot | undefined): boolean | UrlTree | Observable<boolean | UrlTree> | Promise<boolean | UrlTree> {
    const seconds = new Date().getSeconds()
    console.log('DateGuard canDectivate', { seconds })
    return seconds % 2 === 0;
  }
  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
    const seconds = new Date().getSeconds()
    console.log('DateGuard canActivate', { seconds })
    return seconds % 2 === 0;
  }

}
