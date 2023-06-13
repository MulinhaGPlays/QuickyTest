import { ComponentFixture, TestBed } from '@angular/core/testing';

import { QuickyTestComponent } from './quicky-test.component';

describe('QuickyTestComponent', () => {
  let component: QuickyTestComponent;
  let fixture: ComponentFixture<QuickyTestComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ QuickyTestComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(QuickyTestComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
