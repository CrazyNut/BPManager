import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProcessElementComponent } from './process-element.component';

describe('ProcessElementComponent', () => {
  let component: ProcessElementComponent;
  let fixture: ComponentFixture<ProcessElementComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ProcessElementComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ProcessElementComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
