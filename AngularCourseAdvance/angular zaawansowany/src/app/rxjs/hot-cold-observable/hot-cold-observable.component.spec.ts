import { ComponentFixture, TestBed } from '@angular/core/testing';
import { MaterialModule } from 'src/app/material/material.module';

import { HotColdObservableComponent } from './hot-cold-observable.component';

describe('HotColdObservableComponent', () => {
  let component: HotColdObservableComponent;
  let fixture: ComponentFixture<HotColdObservableComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [MaterialModule],
      declarations: [HotColdObservableComponent]
    })
      .compileComponents();

    fixture = TestBed.createComponent(HotColdObservableComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
