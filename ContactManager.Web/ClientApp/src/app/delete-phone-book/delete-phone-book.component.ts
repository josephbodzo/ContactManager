import { Component, OnInit, Inject, OnDestroy } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap/modal/bs-modal-ref.service';
import { HttpClient } from '@angular/common/http';
import { IPhoneBook } from '../models/phoneBook.model';
import { BsModalService } from 'ngx-bootstrap/modal';
import { ConfirmActionComponent } from "../confirm-action/confirm-action.component";
import { EnumConfirmationResult } from "../shared/enums/confirmation-result.enum";
import { Subscription } from 'rxjs';
import { CONSTANT_RELOAD_PHONE_BOOKS } from "../shared/contants";

@Component({
  selector: 'app-delete-phone-book',
  templateUrl: './delete-phone-book.component.html',
  styleUrls: ['./delete-phone-book.component.css']
})
export class DeletePhoneBookComponent implements OnInit, OnDestroy {
  constructor(public bsModalRef: BsModalRef, private http: HttpClient, @Inject('BASE_URL') private baseUrl: string,
    private modalService: BsModalService) { }
  deleteSuccessful: boolean;
  errorMessage: string;
  phoneBook: IPhoneBook;
  subscription: Subscription;

  ngOnInit() {
    this.subscription =  this.modalService.onHide.subscribe((reason: string) => {
      var result: EnumConfirmationResult = EnumConfirmationResult[reason];
      if (result === EnumConfirmationResult.Confirmed)
        this.deleteBook(this.phoneBook.id);
      else
        this.errorMessage = "Action has been cancelled";
    });
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }

  showConfirmation() {
    const initialState = {
      message: "Phone Entries will be deleted if they only exist in the selected phone book, " +
        "otherwise the entries will just be unlinked from the phone book. Do you want to proceed?"
    };
    this.modalService.show(ConfirmActionComponent, { initialState});
  }

  deleteBook(id) {
    this.errorMessage = "";

    this.http.delete(this.baseUrl + `api/phonebooks/${id}`).subscribe(result => {
      this.deleteSuccessful = true;
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
