import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProcessElementActionsComponent } from './process-element-actions.component';

describe('ProcessElementActionsComponent', () => {
  let component: ProcessElementActionsComponent;
  let fixture: ComponentFixture<ProcessElementActionsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ProcessElementActionsComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ProcessElementActionsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
