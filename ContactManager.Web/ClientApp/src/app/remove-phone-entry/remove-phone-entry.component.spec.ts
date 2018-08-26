import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { RemovePhoneEntryComponent } from './remove-phone-entry.component';

describe('RemovePhoneEntryComponent', () => {
  let component: RemovePhoneEntryComponent;
  let fixture: ComponentFixture<RemovePhoneEntryComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ RemovePhoneEntryComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RemovePhoneEntryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
