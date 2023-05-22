import { ComponentFixture, TestBed } from '@angular/core/testing';
import { NoopAnimationsModule } from '@angular/platform-browser/animations';
import { RouterTestingModule } from '@angular/router/testing';
import { MaterialModule } from 'src/app/material/material.module';

import { PiaskownicaComponent } from './piaskownica.component';

describe('PiaskownicaComponent', () => {
  let component: PiaskownicaComponent;
  let fixture: ComponentFixture<PiaskownicaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [MaterialModule, RouterTestingModule, NoopAnimationsModule],
      declarations: [PiaskownicaComponent]
    })
      .compileComponents();

    fixture = TestBed.createComponent(PiaskownicaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
