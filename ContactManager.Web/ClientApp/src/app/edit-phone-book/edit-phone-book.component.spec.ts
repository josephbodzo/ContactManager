import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EditPhoneBookComponent } from './edit-phone-book.component';

describe('EditPhoneBookComponent', () => {
  let component: EditPhoneBookComponent;
  let fixture: ComponentFixture<EditPhoneBookComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EditPhoneBookComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EditPhoneBookComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
