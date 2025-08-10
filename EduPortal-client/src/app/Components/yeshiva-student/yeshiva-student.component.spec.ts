import { ComponentFixture, TestBed } from '@angular/core/testing';

import { YeshivaStudentComponent } from './yeshiva-student.component';

describe('YeshivaStudentComponent', () => {
  let component: YeshivaStudentComponent;
  let fixture: ComponentFixture<YeshivaStudentComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [YeshivaStudentComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(YeshivaStudentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
