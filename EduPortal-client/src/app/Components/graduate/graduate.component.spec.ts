import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GraduateComponent } from './graduate.component';

describe('GraduateComponent', () => {
  let component: GraduateComponent;
  let fixture: ComponentFixture<GraduateComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [GraduateComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(GraduateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
