import { Component, OnInit, Inject, OnDestroy } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap/modal/bs-modal-ref.service';
import { HttpClient } from '@angular/common/http';
import { IPhoneEntry } from '../models/phoneEntry.model';
import { BsModalService } from 'ngx-bootstrap/modal';
import { ConfirmActionComponent } from "../confirm-action/confirm-action.component";
import { EnumConfirmationResult } from "../shared/enums/confirmation-result.enum";
import { Subscription } from 'rxjs';
import { CONSTANT_RELOAD_PHONE_ENTRIES } from "../shared/contants";

@Component({
  selector: 'app-remove-phone-entry',
  templateUrl: './remove-phone-entry.component.html',
  styleUrls: ['./remove-phone-entry.component.css']
})
export class RemovePhoneEntryComponent implements OnInit, OnDestroy {
  constructor(public bsModalRef: BsModalRef, private readonly http: HttpClient, @Inject('BASE_URL') private baseUrl: string,
    private modalService: BsModalService) { }
  deleteSuccessful: boolean;
  errorMessage: string;
  phoneEntry: IPhoneEntry;
  subscription: Subscription;

  ngOnInit() {
    this.subscription = this.modalService.onHide.subscribe((reason: string) => {
      var result: EnumConfirmationResult = EnumConfirmationResult[reason];
      if (result === EnumConfirmationResult.Confirmed)
        this.removeEntry(this.phoneEntry.id);
      else
        this.errorMessage = "Action has been cancelled";
    });
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }

  showConfirmation() {
    const initialState = {
      message: "Phone Entry will be deleted if it only exists in selected phone book, " +
        "otherwise it will just be unlinked from this phone book.  Do you want to proceed?"
    };
    this.modalService.show(ConfirmActionComponent, { initialState});
  }

  removeEntry(id) {
    this.errorMessage = "";
    this.http.delete(this.baseUrl + `api/phoneentries/${id}`).subscribe(result => {
      this.deleteSuccessful = true;
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
