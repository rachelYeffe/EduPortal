import { TestBed } from '@angular/core/testing';

import { YeshivaStudentService } from './yeshiva-student.service';

describe('YeshivaStudentService', () => {
  let service: YeshivaStudentService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(YeshivaStudentService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
