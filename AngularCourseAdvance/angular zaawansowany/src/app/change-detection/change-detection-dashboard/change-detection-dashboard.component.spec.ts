import { ComponentFixture, TestBed } from '@angular/core/testing';
import { MaterialModule } from 'src/app/material/material.module';
import { ChangeDetectionOnpushComponent } from '../change-detection-onpush/change-detection-onpush.component';
import { SlowNumbersComponent } from '../slow-numbers/slow-numbers.component';

import { ChangeDetectionDashboardComponent } from './change-detection-dashboard.component';

describe('ChangeDetectionDashboardComponent', () => {
  let component: ChangeDetectionDashboardComponent;
  let fixture: ComponentFixture<ChangeDetectionDashboardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [MaterialModule],
      declarations: [ChangeDetectionDashboardComponent, ChangeDetectionOnpushComponent, SlowNumbersComponent]
    })
      .compileComponents();

    fixture = TestBed.createComponent(ChangeDetectionDashboardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
