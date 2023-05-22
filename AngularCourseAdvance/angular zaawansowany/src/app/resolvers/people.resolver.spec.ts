import { HttpClientTestingModule } from '@angular/common/http/testing';
import { TestBed } from '@angular/core/testing';
import { SwApiService } from '../services/sw-api/sw-api.service';

import { PeopleResolver } from './people.resolver';

describe('PeopleResolver', () => {
  let resolver: PeopleResolver;

  // SwApiTestService

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [SwApiService]
    });
    resolver = TestBed.inject(PeopleResolver);
  });

  it('should be created', () => {
    expect(resolver).toBeTruthy();
  });
});
