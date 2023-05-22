import { TestBed } from '@angular/core/testing';

import { TimeoutResolver } from './timeout.resolver';

describe('TimeoutResolver', () => {
  let resolver: TimeoutResolver;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    resolver = TestBed.inject(TimeoutResolver);
  });

  it('should be created', () => {
    expect(resolver).toBeTruthy();
  });
});
