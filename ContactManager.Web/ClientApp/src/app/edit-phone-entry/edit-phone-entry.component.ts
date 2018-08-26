import { Component, OnInit, Inject, OnDestroy } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap/modal/bs-modal-ref.service';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { IPhoneEntry } from '../models/phoneEntry.model';
import { BsModalService } from 'ngx-bootstrap/modal';
import { ConfirmActionComponent } from "../confirm-action/confirm-action.component";
import { EnumConfirmationResult } from "../shared/enums/confirmation-result.enum";
import { Subscription } from 'rxjs';
import { CONSTANT_RELOAD_PHONE_ENTRIES, CONSTANT_PHONE_NUMBER_REGEX } from "../shared/constants";

@Component({
  selector: 'app-edit-phone-entry',
  templateUrl: './edit-phone-entry.component.html',
  styleUrls: ['./edit-phone-entry.component.css']
})
export class EditPhoneEntryComponent implements OnInit, OnDestroy {
  constructor(public bsModalRef: BsModalRef, private readonly http: HttpClient, @Inject('BASE_URL') private baseUrl: string,
    private modalService: BsModalService) { }
  name: FormControl;
  phoneNumber: FormControl;
  entryForm: FormGroup;
  saveSuccessful: boolean;
  errorMessage: string;
  phoneEntry: IPhoneEntry;
  subscription: Subscription;

  ngOnInit() {
    //TODO: display book from server (with latest info) instead of taking from grid
    this.name = new FormControl(this.phoneEntry.name, [Validators.required, Validators.minLength(5), Validators.maxLength(50)]);
    this.phoneNumber = new FormControl(this.phoneEntry.phoneNumber, [Validators.required, Validators.pattern(CONSTANT_PHONE_NUMBER_REGEX)]);
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

  ngOnDestroy(): void {
   this.subscription.unsubscribe();
  }

  showConfirmation() {
    this.modalService.show(ConfirmActionComponent);
  }

  isNameValid() {
    return this.name.valid || this.name.pristine;
  }

  isPhoneNumberValid() {
    return this.phoneNumber.valid || this.phoneNumber.pristine;
  }

  saveEntry(formValues) {
    formValues.id = this.phoneEntry.id;
    this.errorMessage = "";
    this.http.put(this.baseUrl + 'api/phoneentries', formValues).subscribe(result => {
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
