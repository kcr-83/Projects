import { ComponentFixture, TestBed } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { MaterialModule } from 'src/app/material/material.module';

import { RouterEventsComponent } from './router-events.component';

describe('RouterEventsComponent', () => {
  let component: RouterEventsComponent;
  let fixture: ComponentFixture<RouterEventsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [MaterialModule, RouterTestingModule],
      declarations: [RouterEventsComponent]
    })
      .compileComponents();

    fixture = TestBed.createComponent(RouterEventsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
