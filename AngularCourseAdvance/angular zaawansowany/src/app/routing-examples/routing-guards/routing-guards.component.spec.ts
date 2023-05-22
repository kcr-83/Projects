import { ComponentFixture, TestBed } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { MaterialModule } from 'src/app/material/material.module';

import { RoutingGuardsComponent } from './routing-guards.component';

describe('RoutingGuardsComponent', () => {
  let component: RoutingGuardsComponent;
  let fixture: ComponentFixture<RoutingGuardsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [MaterialModule, RouterTestingModule],
      declarations: [RoutingGuardsComponent]
    })
      .compileComponents();

    fixture = TestBed.createComponent(RoutingGuardsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
