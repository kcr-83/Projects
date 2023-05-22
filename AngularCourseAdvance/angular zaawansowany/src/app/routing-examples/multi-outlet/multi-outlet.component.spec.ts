import { ComponentFixture, TestBed } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { MaterialModule } from 'src/app/material/material.module';

import { MultiOutletComponent } from './multi-outlet.component';

describe('MultiOutletComponent', () => {
  let component: MultiOutletComponent;
  let fixture: ComponentFixture<MultiOutletComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [MaterialModule, RouterTestingModule],
      declarations: [MultiOutletComponent]
    })
      .compileComponents();

    fixture = TestBed.createComponent(MultiOutletComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
