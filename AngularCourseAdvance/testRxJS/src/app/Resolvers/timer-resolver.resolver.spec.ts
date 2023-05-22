import { TestBed } from '@angular/core/testing';

import { TimerResolverResolver } from './timer-resolver.resolver';

describe('TimerResolverResolver', () => {
  let resolver: TimerResolverResolver;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    resolver = TestBed.inject(TimerResolverResolver);
  });

  it('should be created', () => {
    expect(resolver).toBeTruthy();
  });
});
