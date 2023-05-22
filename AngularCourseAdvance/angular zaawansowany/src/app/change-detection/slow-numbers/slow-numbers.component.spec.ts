import { ComponentFixture, TestBed } from '@angular/core/testing';
import { MaterialModule } from 'src/app/material/material.module';

import { SlowNumbersComponent } from './slow-numbers.component';

describe('SlowListComponent', () => {
  let component: SlowNumbersComponent;
  let fixture: ComponentFixture<SlowNumbersComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [MaterialModule],
      declarations: [SlowNumbersComponent]
    })
      .compileComponents();

    fixture = TestBed.createComponent(SlowNumbersComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
