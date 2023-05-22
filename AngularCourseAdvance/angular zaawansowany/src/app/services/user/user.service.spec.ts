import { TestBed } from '@angular/core/testing';
import { UserRole } from './user-roles.enum';

import { UserService } from './user.service';

describe('UserService', () => {
  let service: UserService;

  beforeEach(() => {
    // TestBed.configureTestingModule({});
    // service = TestBed.inject(UserService);
  });
  beforeAll(() => {
    service = new UserService();
    // TestBed.configureTestingModule({});
    // service = TestBed.inject(UserService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
  it('should return User rol', () => {
    const userRole = service.getRole()
    expect(userRole).toBe(UserRole.Admin)
  });
});
