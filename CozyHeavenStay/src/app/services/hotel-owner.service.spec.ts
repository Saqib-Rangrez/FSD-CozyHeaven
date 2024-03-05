import { TestBed } from '@angular/core/testing';

import { HotelOwnerService } from './hotel-owner.service';

describe('HotelOwnerService', () => {
  let service: HotelOwnerService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(HotelOwnerService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
