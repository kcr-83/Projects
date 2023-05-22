import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MessageComponent } from 'src/app/app-components/message/message.component';
import { DateGuard } from 'src/app/guards/date/date.guard';
import { UserRoleAdminGuard } from 'src/app/guards/user-role-admin/user-role-admin.guard';
import { UserRoleGuard } from 'src/app/guards/user-role/user-role.guard';
import { UserRole } from 'src/app/services/user/user-roles.enum';
import { RoutingGuardsComponent } from './routing-guards.component';

const routes: Routes = [
  {
    path: '',
    component: RoutingGuardsComponent,
    children: [
      {
        path: 'canactivate',
        data: { message: 'Jesteś Bogiem:)' },
        title: 'CanActivate',
        canActivate: [UserRoleAdminGuard],

        component: MessageComponent
      },
      {
        path: 'canactivate-multi',
        data: { roles: [UserRole.Moderator, UserRole.Admin], message: 'Jesteś modem lub adminem!' },
        title: 'CanActivate',
        canActivate: [UserRoleGuard],
        // canLoad: [UserRoleGuard, UserLocationFirm],
        component: MessageComponent
      },
      {
        path: 'candeactivate',
        data: { roles: [UserRole.Moderator, UserRole.Admin], message: 'Wyjdziesz tylko jeśli sekundy%2 === 0' },
        title: 'CanDeactivate',
        canActivate: [DateGuard],
        canDeactivate: [DateGuard],
        component: MessageComponent
      },
    ]
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class RoutingGuardsRoutingModule {}
