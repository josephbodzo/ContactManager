import { Component, OnInit, Inject } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap/modal/bs-modal-ref.service';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { IPhoneEntry } from '../models/phoneEntry.model';

@Component({
  selector: 'app-edit-phone-entry',
  templateUrl: './edit-phone-entry.component.html',
  styleUrls: ['./edit-phone-entry.component.css']
})
export class EditPhoneEntryComponent implements OnInit {
  constructor(public bsModalRef: BsModalRef, private readonly http: HttpClient, @Inject('BASE_URL') private baseUrl: string, ) { }
  name: FormControl;
  phoneNumber: FormControl;
  entryForm: FormGroup;
  saveSuccessful: boolean;
  errorMessage: string;
  phoneEntry: IPhoneEntry;

  ngOnInit() {
    //TODO: display book from server (with latest info) instead of taking from grid
    this.name = new FormControl(this.phoneEntry.name, Validators.required);
    this.phoneNumber = new FormControl(this.phoneEntry.phoneNumber, Validators.required);
    this.entryForm = new FormGroup({
      name: this.name,
      phoneNumber: this.phoneNumber
    });
  }

  saveEntry(formValues) {
    formValues.id = this.phoneEntry.id;
    this.errorMessage = "";
    this.http.put(this.baseUrl + 'api/phoneentries', formValues).subscribe(result => {
        this.saveSuccessful = true;
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
