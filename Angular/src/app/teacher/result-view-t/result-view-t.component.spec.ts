import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ResultViewTComponent } from './result-view-t.component';

describe('ResultViewTComponent', () => {
  let component: ResultViewTComponent;
  let fixture: ComponentFixture<ResultViewTComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ResultViewTComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ResultViewTComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
