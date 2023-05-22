import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LongLoadingComponent } from './long-loading.component';

describe('LongLoadingComponent', () => {
  let component: LongLoadingComponent;
  let fixture: ComponentFixture<LongLoadingComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ LongLoadingComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(LongLoadingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
