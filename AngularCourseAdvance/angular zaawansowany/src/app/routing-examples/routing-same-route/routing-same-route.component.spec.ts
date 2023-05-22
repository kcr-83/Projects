import { ComponentFixture, TestBed } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { MaterialModule } from 'src/app/material/material.module';

import { RoutingSameRouteComponent } from './routing-same-route.component';

describe('RoutingSameRouteComponent', () => {
  let component: RoutingSameRouteComponent;
  let fixture: ComponentFixture<RoutingSameRouteComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [MaterialModule, RouterTestingModule],
      declarations: [RoutingSameRouteComponent]
    })
      .compileComponents();

    fixture = TestBed.createComponent(RoutingSameRouteComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
