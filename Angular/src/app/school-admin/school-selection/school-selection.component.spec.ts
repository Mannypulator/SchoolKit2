import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SchoolSelectionComponent } from './school-selection.component';

describe('SchoolSelectionComponent', () => {
  let component: SchoolSelectionComponent;
  let fixture: ComponentFixture<SchoolSelectionComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SchoolSelectionComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SchoolSelectionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
