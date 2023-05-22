import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MouseCoordinatesComponent } from './mouse-coordinates.component';

describe('MouseCoordinatesComponent', () => {
  let component: MouseCoordinatesComponent;
  let fixture: ComponentFixture<MouseCoordinatesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MouseCoordinatesComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(MouseCoordinatesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
