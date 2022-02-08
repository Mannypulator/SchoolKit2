import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ResultViewSComponent } from './result-view-s.component';

describe('ResultViewSComponent', () => {
  let component: ResultViewSComponent;
  let fixture: ComponentFixture<ResultViewSComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ResultViewSComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ResultViewSComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
