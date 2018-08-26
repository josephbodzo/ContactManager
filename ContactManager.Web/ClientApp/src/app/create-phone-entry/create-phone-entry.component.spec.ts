import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CreatePhoneEntryComponent } from './create-phone-entry.component';

describe('CreatePhoneEntryComponent', () => {
  let component: CreatePhoneEntryComponent;
  let fixture: ComponentFixture<CreatePhoneEntryComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CreatePhoneEntryComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CreatePhoneEntryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
