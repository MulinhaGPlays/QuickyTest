import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProofLayoutComponent } from './proof-layout.component';

describe('ProofLayoutComponent', () => {
  let component: ProofLayoutComponent;
  let fixture: ComponentFixture<ProofLayoutComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ProofLayoutComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ProofLayoutComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
