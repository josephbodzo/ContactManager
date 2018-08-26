import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DeletePhoneBookComponent } from './delete-phone-book.component';

describe('DeletePhoneBookComponent', () => {
  let component: DeletePhoneBookComponent;
  let fixture: ComponentFixture<DeletePhoneBookComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DeletePhoneBookComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DeletePhoneBookComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
