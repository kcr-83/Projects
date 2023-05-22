import { TestBed } from '@angular/core/testing';

import { UserRoleAdminGuard } from './user-role-admin.guard';

describe('UserRoleAdminGuard', () => {
  let guard: UserRoleAdminGuard;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    guard = TestBed.inject(UserRoleAdminGuard);
  });

  it('should be created', () => {
    expect(guard).toBeTruthy();
  });
});
