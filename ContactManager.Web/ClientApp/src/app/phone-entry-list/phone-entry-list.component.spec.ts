import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PhoneEntryListComponent } from './phone-entry-list.component';

describe('PhoneEntryListComponent', () => {
  let component: PhoneEntryListComponent;
  let fixture: ComponentFixture<PhoneEntryListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PhoneEntryListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PhoneEntryListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
