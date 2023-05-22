import { NgModule } from '@angular/core';
import { RouterModule, Routes, UrlSegment } from '@angular/router';
import { MessageComponent } from '../app-components/message/message.component';
import { PeopleResolver } from '../resolvers/people.resolver';
import { RandomValueResolver } from '../resolvers/random-value.resolver';
import { TimeoutResolver } from '../resolvers/timeout.resolver';
import { MultiOutletComponent } from './multi-outlet/multi-outlet.component';
import { RefreshViewComponent } from './refresh-view/refresh-view.component';
import { RouterEventsComponent } from './router-events/router-events.component';
import { RoutingDashboardComponent } from './routing-dashboard/routing-dashboard.component';
import { RoutingParamsComponent } from './routing-params/routing-params.component';
import { RoutingSameRouteComponent } from './routing-same-route/routing-same-route.component';

const routes: Routes = [
  {
    path: '',
    data: { pageDescription: 'routing krok po kroku' },
    title: 'Routing',
    component: RoutingDashboardComponent,
    children: [
      {
        // 'same-route/category/10/sort/0',
        // 'same-route/category/car/sort/asc',
        path: 'same-route/category/:category/sort/:sort',
        // pathMatch: 'full',
        title: 'Nawigacja po tym samym route',
        component: RoutingSameRouteComponent
      },
      // explicite bez parametrów sort i category (jako opcjonalne)
      {
        path: 'same-route',
        title: 'Nawigacja po tym samym route',
        component: RoutingSameRouteComponent
      },
      {
        path: 'category/:category',
        title: 'Routing z parametrem',
        component: RoutingParamsComponent,
        children: [
          // drugie id w paramsach
          // to już idzie w <router-outlet> RoutingParamsComponent
          { path: 'type/:param2/id/:id', component: RoutingParamsComponent },
        ]
      },
      {
        path: 'events',
        title: 'Zdarzenia routera',
        // resolve: { people: PeopleResolver },
        component: RouterEventsComponent,
        children: [
          {
            path: 'message',
            data: { message: 'Komponent 1', sentencesCount: 2, showButtons: true },
            title: 'Pierwsza wiadomość',
            component: MessageComponent,
          },
          {
            path: 'message2',
            data: { message: 'Komponent 2', sentencesCount: 2, showButtons: true },
            title: 'Druga wiadomość',
            component: MessageComponent,
          }
        ]
      },
      {
        path: 'multi',
        title: 'Multi outltety',
        loadChildren: () => import('./routing-multi-outlets/routing-multi-outlets.module').then(m => m.RoutingMultiOutletsModule)
      },
      {
        path: 'guards',
        title: 'Guards',
        // data: { reuseRoute: true, timeout: 3000 },
        // resolve: { resolveData: TimeoutResolver },
        loadChildren: () => import('./routing-guards/routing-guards.module').then(m => m.RoutingGuardsModule)
      },
      {
        path: 'refresh-view',
        title: 'Odświeżanie widoku bez F5',
        resolve: { randomValue: RandomValueResolver },
        // runGuardsAndResolvers: 'always',
        component: RefreshViewComponent
      },
      {
        matcher: freeMatcher,
        data: { message: 'Nie ma za darmo. Inflacja zjadła.', sentencesCount: 3 },
        component: MessageComponent
      },
    ]
  },
];
function freeMatcher(url: UrlSegment[]) {
  return url.length > 0 && ['free', 'darmo'].some(el => el === url[0].path) ? ({ consumed: url }) : null;
}

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class RoutingExamplesRoutingModule {}
