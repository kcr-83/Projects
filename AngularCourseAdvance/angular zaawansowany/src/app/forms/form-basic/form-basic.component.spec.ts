import { ComponentFixture, TestBed } from '@angular/core/testing';
import { FormBuilder, ReactiveFormsModule } from '@angular/forms';
import { NoopAnimationsModule } from '@angular/platform-browser/animations';
import { MaterialModule } from 'src/app/material/material.module';

import { FormBasicComponent } from './form-basic.component';

describe('FormBasicComponent', () => {
  let component: FormBasicComponent;
  let fixture: ComponentFixture<FormBasicComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [MaterialModule, NoopAnimationsModule, ReactiveFormsModule],
      providers: [FormBuilder],
      declarations: [FormBasicComponent]
    })
      .compileComponents();

    fixture = TestBed.createComponent(FormBasicComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
