import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CreatePhoneBookComponent } from './create-phone-book.component';

describe('CreatePhoneBookComponent', () => {
  let component: CreatePhoneBookComponent;
  let fixture: ComponentFixture<CreatePhoneBookComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CreatePhoneBookComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CreatePhoneBookComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
