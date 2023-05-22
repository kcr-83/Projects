import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MessageComponent } from 'src/app/app-components/message/message.component';
import { MultiOutletComponent } from '../multi-outlet/multi-outlet.component';

const routes: Routes = [
  {
    path: '',
    title: 'Multi outltety',
    component: MultiOutletComponent,
    children: [
      {
        path: 'users',
        data: { message: 'Users list', list: ['Leon', 'Matylda', 'Tony'] },
        title: 'Użyszkodnicy',
        component: MessageComponent,
      },
      {
        path: 'users/leon',
        data: { message: 'Leon to jest gość!' },
        outlet: 'szczegoly',
        title: 'Użyszkodnicy',
        component: MessageComponent,
      },
      {
        path: 'cars',
        data: { message: 'Cars list', list: ['Fiat', 'Ford', 'Fskoda'] },
        title: 'Samocho Dziki',
        component: MessageComponent,
      }
    ]
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class RoutingMultiOutletsRoutingModule {}
