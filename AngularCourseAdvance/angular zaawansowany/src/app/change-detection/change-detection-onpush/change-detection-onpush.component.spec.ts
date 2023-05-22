import { ComponentFixture, TestBed } from '@angular/core/testing';
import { MaterialModule } from 'src/app/material/material.module';
import { SlowNumbersComponent } from '../slow-numbers/slow-numbers.component';

import { ChangeDetectionOnpushComponent } from './change-detection-onpush.component';

describe('ChangeDetectionOnpushComponent', () => {
  let component: ChangeDetectionOnpushComponent;
  let fixture: ComponentFixture<ChangeDetectionOnpushComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [MaterialModule],
      declarations: [ChangeDetectionOnpushComponent, SlowNumbersComponent]
    })
      .compileComponents();

    fixture = TestBed.createComponent(ChangeDetectionOnpushComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
