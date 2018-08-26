import { Component, OnInit, Inject, OnDestroy } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap/modal/bs-modal-ref.service';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { BsModalService } from 'ngx-bootstrap/modal';
import { ConfirmActionComponent } from "../confirm-action/confirm-action.component";
import { EnumConfirmationResult } from "../shared/enums/confirmation-result.enum";
import { Subscription } from 'rxjs';
import { CONSTANT_RELOAD_PHONE_BOOKS } from "../shared/constants";

@Component({
  selector: 'app-create-phone-book',
  templateUrl: './create-phone-book.component.html',
  styleUrls: ['./create-phone-book.component.css']
})
export class CreatePhoneBookComponent implements OnInit, OnDestroy {
  constructor(public bsModalRef: BsModalRef, private http: HttpClient, @Inject('BASE_URL') private baseUrl: string,
    private modalService: BsModalService) { }
  name: FormControl;
  bookForm: FormGroup;
  saveSuccessful: boolean;
  errorMessage: string;
  subscription: Subscription;

  ngOnInit() {
    this.name = new FormControl(null, [Validators.required, Validators.minLength(5), Validators.maxLength(50)]);
    this.bookForm = new FormGroup({
      name: this.name
    });

    this.subscription = this.modalService.onHide.subscribe((reason: string) => {
      var result: EnumConfirmationResult = EnumConfirmationResult[reason];
      if (result === EnumConfirmationResult.Confirmed)
        this.saveBook(this.bookForm.value);
      else
        this.errorMessage = "Action has been cancelled";
    });

  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }

  showConfirmation() {
    this.modalService.show(ConfirmActionComponent);
  }

  isNameValid() {
    return this.name.valid || this.name.pristine;
  }

  saveBook(formValues) {
    this.errorMessage = "";
    this.http.post(this.baseUrl + 'api/phonebooks', formValues).subscribe(result => {
      this.saveSuccessful = true;
        this.modalService.setDismissReason(CONSTANT_RELOAD_PHONE_BOOKS);
        setTimeout(() => this.bsModalRef.hide(), 1000);
      },
      error => {
        if (error.error && error.error.includes && !error.error.includes("DOCTYPE"))
          this.errorMessage = error.error;
        else
          this.errorMessage = "Oops! An error occurred whilst processing your request.";
      });
  }
}
