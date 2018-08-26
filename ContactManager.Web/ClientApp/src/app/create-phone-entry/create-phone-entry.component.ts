import { Component, OnInit, Inject } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap/modal/bs-modal-ref.service';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-create-phone-entry',
  templateUrl: './create-phone-entry.component.html',
  styleUrls: ['./create-phone-entry.component.css']
})
export class CreatePhoneEntryComponent implements OnInit {
  constructor(public bsModalRef: BsModalRef, private readonly http: HttpClient, @Inject('BASE_URL') private baseUrl: string, ) { }
  name: FormControl;
  phoneNumber: FormControl;
  entryForm: FormGroup;
  saveSuccessful: boolean;
  errorMessage: string;
  phoneBookId: number;

  ngOnInit() {
    this.name = new FormControl(null, Validators.required);
    this.phoneNumber = new FormControl(null, Validators.required);
    this.entryForm = new FormGroup({
      name: this.name,
      phoneNumber: this.phoneNumber
    });
  }

  saveEntry(formValues) {

    this.errorMessage = "";
    this.http.post(this.baseUrl + `api/phoneentries/${this.phoneBookId}`, formValues).subscribe(result => {
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
