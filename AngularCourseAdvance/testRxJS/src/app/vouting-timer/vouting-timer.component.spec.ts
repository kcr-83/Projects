import { ComponentFixture, TestBed } from '@angular/core/testing';

import { VoutingTimerComponent } from './vouting-timer.component';

describe('VoutingTimerComponent', () => {
  let component: VoutingTimerComponent;
  let fixture: ComponentFixture<VoutingTimerComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ VoutingTimerComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(VoutingTimerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
