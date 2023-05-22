import { ComponentFixture, TestBed } from '@angular/core/testing';
import { MaterialModule } from 'src/app/material/material.module';

import { ChangeDetectionDefaultComponent } from './change-detection-default.component';

describe('ChangeDetectionDefaultComponent', () => {
  let component: ChangeDetectionDefaultComponent;
  let fixture: ComponentFixture<ChangeDetectionDefaultComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [MaterialModule],
      declarations: [ChangeDetectionDefaultComponent]
    })
      .compileComponents();

    fixture = TestBed.createComponent(ChangeDetectionDefaultComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
