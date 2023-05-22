import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';

import { SwApiService } from './sw-api.service';

describe('ApiService', () => {
  let service: SwApiService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule]
    });
    service = TestBed.inject(SwApiService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
