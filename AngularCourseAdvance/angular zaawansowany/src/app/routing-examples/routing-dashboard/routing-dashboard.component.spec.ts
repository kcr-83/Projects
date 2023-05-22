import { ComponentFixture, TestBed } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { MaterialModule } from 'src/app/material/material.module';

import { RoutingDashboardComponent } from './routing-dashboard.component';

describe('RoutingDashboardComponent', () => {
  let component: RoutingDashboardComponent;
  let fixture: ComponentFixture<RoutingDashboardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [MaterialModule, RouterTestingModule],
      declarations: [RoutingDashboardComponent]
    })
      .compileComponents();

    fixture = TestBed.createComponent(RoutingDashboardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
