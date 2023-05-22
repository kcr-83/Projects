import { LongLoadingComponent } from './long-loading/long-loading.component';
import { TimerResolverResolver } from './Resolvers/timer-resolver.resolver';
import { NgModule, Component } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  {
    path: 'long',
    title: 'Long loading',
    data: { timeot: 3000 },
    resolve: { TimerResolverResolver },
    component: LongLoadingComponent
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
