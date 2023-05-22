import { ComponentFixture, TestBed } from '@angular/core/testing';
import { ActivatedRoute } from '@angular/router';
import { RouterTestingModule } from '@angular/router/testing';
import { MaterialModule } from 'src/app/material/material.module';

import { RefreshViewComponent } from './refresh-view.component';

describe('RefreshViewComponent', () => {
  let component: RefreshViewComponent;
  let fixture: ComponentFixture<RefreshViewComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [MaterialModule, RouterTestingModule],
      declarations: [RefreshViewComponent]
    })
      .compileComponents();

    fixture = TestBed.createComponent(RefreshViewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
