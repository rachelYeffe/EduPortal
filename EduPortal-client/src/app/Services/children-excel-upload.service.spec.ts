import { TestBed } from '@angular/core/testing';

import { ChildrenExcelUploadService } from './children-excel-upload.service';

describe('ChildrenExcelUploadService', () => {
  let service: ChildrenExcelUploadService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ChildrenExcelUploadService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
