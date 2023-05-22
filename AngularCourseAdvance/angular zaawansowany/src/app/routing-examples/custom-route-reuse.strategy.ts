import { ActivatedRouteSnapshot, BaseRouteReuseStrategy, DetachedRouteHandle, RouteReuseStrategy } from "@angular/router";

const handles: any[] = []
export class CustomRouteReuseStrategy extends BaseRouteReuseStrategy implements RouteReuseStrategy {
  override shouldReuseRoute(future: ActivatedRouteSnapshot, curr: ActivatedRouteSnapshot): boolean {
    const shouldReuse = (future.routeConfig === curr.routeConfig) || curr.data['reuseRoute'];
    console.log(shouldReuse)
    return shouldReuse
  }

}