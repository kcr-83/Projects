import { Injectable } from '@angular/core';
import {
  Router, Resolve,
  RouterStateSnapshot,
  ActivatedRouteSnapshot
} from '@angular/router';
import { map, Observable, of, timer } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class TimeoutResolver implements Resolve<boolean> {
  resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<boolean> {
    const timeout = route.data['timeout'] || 4000;
    return timer(timeout).pipe(map(() => true));
  }
}
