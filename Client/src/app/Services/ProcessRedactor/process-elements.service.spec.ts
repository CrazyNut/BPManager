import { TestBed } from '@angular/core/testing';

import { ProcessElementsService } from './process-elements.service';

describe('ProcessElementsService', () => {
  let service: ProcessElementsService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ProcessElementsService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
