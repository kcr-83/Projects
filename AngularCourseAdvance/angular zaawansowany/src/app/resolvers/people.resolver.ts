import { Injectable } from '@angular/core';
import {
  Router, Resolve,
  RouterStateSnapshot,
  ActivatedRouteSnapshot
} from '@angular/router';
import { Observable, of } from 'rxjs';
import { SwApiPeople } from '../models/sw-api-people.model';
import { SwApiService } from '../services/sw-api/sw-api.service';

@Injectable({
  providedIn: 'root'
})
export class PeopleResolver implements Resolve<SwApiPeople> {
  constructor(private readonly api: SwApiService) {}
  resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<SwApiPeople> {
    const people = this.api.getPeople();
    return people;
  }
}
