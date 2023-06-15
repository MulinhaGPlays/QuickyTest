import { ComponentFixture, TestBed } from '@angular/core/testing';

import { VisualProofComponent } from './visual-proof.component';

describe('VisualProofComponent', () => {
  let component: VisualProofComponent;
  let fixture: ComponentFixture<VisualProofComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ VisualProofComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(VisualProofComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
