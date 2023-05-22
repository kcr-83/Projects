import { ComponentFixture, TestBed } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { MaterialModule } from 'src/app/material/material.module';

import { ComponentsDashboardComponent } from './components-dashboard.component';

describe('ComponentsDashboardComponent', () => {
  let component: ComponentsDashboardComponent;
  let fixture: ComponentFixture<ComponentsDashboardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [MaterialModule, RouterTestingModule],
      declarations: [ComponentsDashboardComponent]
    })
      .compileComponents();

    fixture = TestBed.createComponent(ComponentsDashboardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
