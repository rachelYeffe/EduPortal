import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ChildrenExcelUploadComponent } from './children-excel-upload.component';

describe('ChildrenExcelUploadComponent', () => {
  let component: ChildrenExcelUploadComponent;
  let fixture: ComponentFixture<ChildrenExcelUploadComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ChildrenExcelUploadComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ChildrenExcelUploadComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
