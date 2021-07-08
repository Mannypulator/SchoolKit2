import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RemoveTeacherComponent } from './remove-teacher.component';

describe('RemoveTeacherComponent', () => {
  let component: RemoveTeacherComponent;
  let fixture: ComponentFixture<RemoveTeacherComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ RemoveTeacherComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(RemoveTeacherComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
