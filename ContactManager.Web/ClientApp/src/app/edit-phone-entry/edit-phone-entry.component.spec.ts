import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EditPhoneEntryComponent } from './edit-phone-entry.component';

describe('EditPhoneEntryComponent', () => {
  let component: EditPhoneEntryComponent;
  let fixture: ComponentFixture<EditPhoneEntryComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EditPhoneEntryComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EditPhoneEntryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
