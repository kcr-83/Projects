import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ContentProjectionDashboardComponent } from './content-projection-dashboard.component';

describe('ContentProjectionDashboardComponent', () => {
  let component: ContentProjectionDashboardComponent;
  let fixture: ComponentFixture<ContentProjectionDashboardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ContentProjectionDashboardComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ContentProjectionDashboardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
