import { ComponentFixture, TestBed } from '@angular/core/testing';
import { MaterialModule } from 'src/app/material/material.module';

import { ManyObservablesComponent } from './many-observables.component';

describe('ManyObservablesComponent', () => {
  let component: ManyObservablesComponent;
  let fixture: ComponentFixture<ManyObservablesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [MaterialModule],
      declarations: [ManyObservablesComponent]
    })
      .compileComponents();

    fixture = TestBed.createComponent(ManyObservablesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
