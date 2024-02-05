import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProdusComponent } from './produs.componet';

describe('ProdusComponent', () => {
  let component: ProdusComponent;
  let fixture: ComponentFixture<ProdusComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ProdusComponent]
    })
      .compileComponents();

    fixture = TestBed.createComponent(ProdusComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
