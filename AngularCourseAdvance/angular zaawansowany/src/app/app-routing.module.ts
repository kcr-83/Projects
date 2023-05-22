import { NgModule } from '@angular/core';
import { ExtraOptions, PreloadAllModules, RouteReuseStrategy, RouterModule, Routes, UrlSegment } from '@angular/router';
import { DashboardComponent } from './app-components/dashboard/dashboard.component';
import { NotFoundComponent } from './app-components/not-found/not-found.component';
import { RouteDataPreloadingStrategy } from './helpers/route-data-preloading-strategy';
import { CustomRouteReuseStrategy } from './routing-examples/custom-route-reuse.strategy';
import { RoutingExamplesModule } from './routing-examples/routing-examples.module';

const routes: Routes = [
  // route do komponentu
  {
    path: '',
    component: DashboardComponent,
  },
  // lazy load
  {
    path: 'rxjs',
    loadChildren: () => import('./rxjs/rxjs.module').then(m => m.RxjsModule)
  },
  {
    path: 'routing',
    loadChildren: () => import('./routing-examples/routing-examples.module').then(m => m.RoutingExamplesModule)
    // immediate module load
    // loadChildren: () => RoutingExamplesModule
  },
  {
    path: 'forms',
    // data: { preloadModule: true },
    loadChildren: () => import('./forms/forms.module').then(m => m.FormsModule)
  },
  {
    path: 'change-detection',
    loadChildren: () => import('./change-detection/change-detection.module').then(m => m.ChangeDetectionModule)
  },
  {
    path: 'http',
    loadChildren: () => import('./http/http.module').then(m => m.HttpModule)
  },
  {
    path: 'content-projection',
    loadChildren: () => import('./content-projection/content-projection.module').then(m => m.ContentProjectionModule)
  },
  {
    path: 'components',
    loadChildren: () => import('./components/components.module').then(m => m.ComponentsModule)
  },
  {
    path: 'piaskownica',
    loadChildren: () => import('./piaskownica/piaskownica.module').then(m => m.PiaskownicaModule)
  },
  // przekierowanie i pathMatch
  // redirect to działa na dopasowaniu częściowym więc potrzebuje pathMatch
  {
    path: 'rx-js',
    // pathMatch: 'full',
    redirectTo: '/blablabla'
  },
  {
    path: 'rx-js/wlasciwy',
    redirectTo: 'rxjs'
  },
  {
    path: 'onet',
    redirectTo: '//https://onet.pl'
  },
  // catchall 
  {
    path: '**', title: 'Tu nawet Jack Sparrow nie dotarł... jeszcze.'
    , component: NotFoundComponent
  },

];
export function freeMatcher(url: UrlSegment[]) {
  return url.length === 2 && ['free', 'darmo'].some(el => el === url[1].path) ? ({ consumed: url }) : null;
}
const routeOptions: ExtraOptions = {
  // onSameUrlNavigation: 'reload',
  // enableTracing: true
  // paramsInheritanceStrategy: "always"
  // preloadingStrategy: PreloadAllModules,
  // preloadingStrategy: RouteDataPreloadingStrategy, //custom
}
@NgModule({
  imports: [RouterModule.forRoot(routes, routeOptions)],
  exports: [RouterModule],
  providers: [
    // RouteDataPreloadingStrategy,
    // {
    //   provide: RouteReuseStrategy,
    // useClass: CustomRouteReuseStrategy
    // }
  ]
})
export class AppRoutingModule {}
