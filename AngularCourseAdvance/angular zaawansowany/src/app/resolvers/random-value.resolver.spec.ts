import { TestBed } from '@angular/core/testing';

import { RandomValueResolver } from './random-value.resolver';

describe('RandomValueResolver', () => {
  let resolver: RandomValueResolver;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    resolver = TestBed.inject(RandomValueResolver);
  });

  it('should be created', () => {
    expect(resolver).toBeTruthy();
  });
});
