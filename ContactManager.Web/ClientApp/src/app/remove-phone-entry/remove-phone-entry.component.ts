import { Component, OnInit, Inject } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap/modal/bs-modal-ref.service';
import { HttpClient } from '@angular/common/http';
import { IPhoneEntry } from '../models/phoneEntry.model';

@Component({
  selector: 'app-remove-phone-entry',
  templateUrl: './remove-phone-entry.component.html',
  styleUrls: ['./remove-phone-entry.component.css']
})
export class RemovePhoneEntryComponent implements OnInit {
  constructor(public bsModalRef: BsModalRef, private readonly http: HttpClient, @Inject('BASE_URL') private baseUrl: string ) { }
  deleteSuccessful: boolean;
  errorMessage: string;
  phoneEntry: IPhoneEntry;

  ngOnInit() {
  }

  removeEntry(id) {
    this.errorMessage = "";
    this.http.delete(this.baseUrl + `api/phoneentries/${id}`).subscribe(result => {
        this.deleteSuccessful = true;
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
