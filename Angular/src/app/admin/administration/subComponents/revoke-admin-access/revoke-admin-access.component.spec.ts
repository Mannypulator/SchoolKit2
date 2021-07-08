import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RevokeAdminAccessComponent } from './revoke-admin-access.component';

describe('RevokeAdminAccessComponent', () => {
  let component: RevokeAdminAccessComponent;
  let fixture: ComponentFixture<RevokeAdminAccessComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ RevokeAdminAccessComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(RevokeAdminAccessComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
