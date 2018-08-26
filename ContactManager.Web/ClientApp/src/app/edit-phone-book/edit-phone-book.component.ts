import { Component, OnInit, Inject, OnDestroy } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap/modal/bs-modal-ref.service';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { IPhoneBook } from '../models/phoneBook.model';
import { BsModalService } from 'ngx-bootstrap/modal';
import { ConfirmActionComponent } from "../confirm-action/confirm-action.component";
import { EnumConfirmationResult } from "../shared/enums/confirmation-result.enum";
import { Subscription } from 'rxjs';
import { CONSTANT_RELOAD_PHONE_BOOKS } from "../shared/contants";

@Component({
  selector: 'app-edit-phone-book',
  templateUrl: './edit-phone-book.component.html',
  styleUrls: ['./edit-phone-book.component.css']
})
export class EditPhoneBookComponent implements OnInit {
  constructor(public bsModalRef: BsModalRef, private http: HttpClient, @Inject('BASE_URL') private baseUrl: string,
    private modalService: BsModalService) { }
  name: FormControl;
  bookForm: FormGroup;
  saveSuccessful: boolean;
  errorMessage: string;
  phoneBook: IPhoneBook;
  subscription: Subscription;

  ngOnInit() {
    //TODO: display book from server (with latest info) instead of taking from grid
    this.name = new FormControl(this.phoneBook.name, Validators.required);
    this.bookForm = new FormGroup({
      name: this.name
    });

    this.subscription =  this.modalService.onHide.subscribe((reason: string) => {
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

  saveBook(formValues) {
    this.errorMessage = "";
    formValues.id = this.phoneBook.id;
    this.http.put(this.baseUrl + `api/phonebooks/`, formValues).subscribe(result => {
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
