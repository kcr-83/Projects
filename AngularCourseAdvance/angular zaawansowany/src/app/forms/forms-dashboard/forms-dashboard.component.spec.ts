import { ComponentFixture, TestBed } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { MaterialModule } from 'src/app/material/material.module';

import { FormsDashboardComponent } from './forms-dashboard.component';

describe('FormsDashboardComponent', () => {
  let component: FormsDashboardComponent;
  let fixture: ComponentFixture<FormsDashboardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [MaterialModule, RouterTestingModule],
      declarations: [FormsDashboardComponent]
    })
      .compileComponents();

    fixture = TestBed.createComponent(FormsDashboardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
