import { Component, OnInit, Inject, OnDestroy } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap/modal/bs-modal-ref.service';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { BsModalService } from 'ngx-bootstrap/modal';
import { ConfirmActionComponent } from "../confirm-action/confirm-action.component";
import { EnumConfirmationResult } from "../shared/enums/confirmation-result.enum";
import { Subscription } from 'rxjs';
import { IPhoneEntry } from '../models/phoneEntry.model';
import { CONSTANT_RELOAD_PHONE_ENTRIES, CONSTANT_PHONE_NUMBER_REGEX } from "../shared/constants";

@Component({
  selector: 'app-create-phone-entry',
  templateUrl: './create-phone-entry.component.html',
  styleUrls: ['./create-phone-entry.component.css']
})
export class CreatePhoneEntryComponent implements OnInit {
  constructor(public bsModalRef: BsModalRef, private readonly http: HttpClient, @Inject('BASE_URL') private baseUrl: string,
    private modalService: BsModalService) { }
  name: FormControl;
  phoneNumber: FormControl;
  entryForm: FormGroup;
  saveSuccessful: boolean;
  errorMessage: string;
  phoneBookId: number;
  subscription: Subscription;

  ngOnInit() {
    this.name = new FormControl(null, [Validators.required, Validators.minLength(5), Validators.maxLength(50)]);
    this.phoneNumber = new FormControl(null, [Validators.required, Validators.pattern(CONSTANT_PHONE_NUMBER_REGEX)]);
    this.entryForm = new FormGroup({
      name: this.name,
      phoneNumber: this.phoneNumber
    });

    this.subscription = this.modalService.onHide.subscribe((reason: string) => {
      var result: EnumConfirmationResult = EnumConfirmationResult[reason];
      if (result === EnumConfirmationResult.Confirmed)
        this.saveEntry(this.entryForm.value);
      else
        this.errorMessage = "Action has been cancelled";
    });
  }

  showConfirmation() {
    const initialState = {
      message: "Are you sure you want to create this phone entry?",
      htmlMessage: null
    };

    this.http.get<IPhoneEntry>(this.baseUrl + `api/phoneentries/getByPhoneNumber?phoneNumber=${this.entryForm.value.phoneNumber}`).subscribe(result => {
      if (this.entryForm.value.name === result.name) {
        initialState.htmlMessage = `An entry with the same name and phone number already exists.
                                    Do you want to add the entry to this book?`;
      } else {
        initialState.htmlMessage = `Phone number already exists under
                              <span class="text-danger">${result.name}</span>.
                              Do you want to update entry's name?`;
      }
     
      initialState.message = null;
      this.modalService.show(ConfirmActionComponent, { initialState });
    },
      error => {
        if (error.status.toString() === "404") {
          this.modalService.show(ConfirmActionComponent, { initialState });
        }
        else if (error.error && error.error.includes && !error.error.includes("DOCTYPE"))
          this.errorMessage = error.error;
        else
          this.errorMessage = "Oops! An error occurred whilst processing your request.";
      });
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }

  isNameValid() {
    return this.name.valid || this.name.pristine;
  }

  isPhoneNumberValid() {
    return this.phoneNumber.valid || this.phoneNumber.pristine;
  }

  saveEntry(formValues) {

    this.errorMessage = "";
    this.http.post(this.baseUrl + `api/phoneentries/${this.phoneBookId}`, formValues).subscribe(result => {
      this.saveSuccessful = true;
      this.modalService.setDismissReason(CONSTANT_RELOAD_PHONE_ENTRIES);
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
