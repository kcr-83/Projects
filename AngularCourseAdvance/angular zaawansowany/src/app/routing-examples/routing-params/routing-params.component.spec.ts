import { ComponentFixture, TestBed } from '@angular/core/testing';
import { ActivatedRoute } from '@angular/router';
import { RouterTestingModule } from '@angular/router/testing';
import { MaterialModule } from 'src/app/material/material.module';

import { RoutingParamsComponent } from './routing-params.component';

describe('RoutingParamsComponent', () => {
  let component: RoutingParamsComponent;
  let fixture: ComponentFixture<RoutingParamsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [MaterialModule, RouterTestingModule],
      declarations: [RoutingParamsComponent]
    })
      .compileComponents();

    fixture = TestBed.createComponent(RoutingParamsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
