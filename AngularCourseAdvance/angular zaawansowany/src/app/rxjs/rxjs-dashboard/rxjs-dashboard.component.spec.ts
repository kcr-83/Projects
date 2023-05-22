import { ComponentFixture, TestBed } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { MaterialModule } from 'src/app/material/material.module';
import { HotColdObservableComponent } from '../hot-cold-observable/hot-cold-observable.component';

import { RxjsDashboardComponent } from './rxjs-dashboard.component';

describe('RxjsComponent', () => {
  let component: RxjsDashboardComponent;
  let fixture: ComponentFixture<RxjsDashboardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [MaterialModule, RouterTestingModule],
      declarations: [RxjsDashboardComponent, HotColdObservableComponent]
    })
      .compileComponents();

    fixture = TestBed.createComponent(RxjsDashboardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
