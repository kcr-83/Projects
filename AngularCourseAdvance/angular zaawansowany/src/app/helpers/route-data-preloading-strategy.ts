import { Injectable } from "@angular/core";
import { PreloadingStrategy, Route } from "@angular/router";
import { Observable, of } from "rxjs";

@Injectable()
export class RouteDataPreloadingStrategy implements PreloadingStrategy {
  preload(route: Route, load: () => Observable<any>): Observable<any> {
    // można dołożyć inne zależności - np. opóźnione ładowanie
    return route.data?.['preloadModule']
      ? load()
      : of(null)
  }
} 