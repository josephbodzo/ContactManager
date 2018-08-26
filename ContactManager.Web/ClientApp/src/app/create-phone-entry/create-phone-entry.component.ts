import { Component, OnInit, Inject, OnDestroy } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap/modal/bs-modal-ref.service';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { BsModalService } from 'ngx-bootstrap/modal';
import { ConfirmActionComponent } from "../confirm-action/confirm-action.component";
import { EnumConfirmationResult } from "../shared/enums/confirmation-result.enum";
import { Subscription } from 'rxjs';
import { CONSTANT_RELOAD_PHONE_ENTRIES } from "../shared/contants";

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
    this.name = new FormControl(null, Validators.required);
    this.phoneNumber = new FormControl(null, Validators.required);
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
    this.modalService.show(ConfirmActionComponent);
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
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
