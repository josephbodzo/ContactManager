import { Component, OnInit, Inject } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap/modal/bs-modal-ref.service';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { IPhoneBook } from '../models/phoneBook.model';

@Component({
  selector: 'app-edit-phone-book',
  templateUrl: './edit-phone-book.component.html',
  styleUrls: ['./edit-phone-book.component.css']
})
export class EditPhoneBookComponent implements OnInit {
  constructor(public bsModalRef: BsModalRef, private http: HttpClient, @Inject('BASE_URL') private baseUrl: string, ) { }
  name: FormControl;
  bookForm: FormGroup;
  saveSuccessful: boolean;
  errorMessage: string;
  phoneBook: IPhoneBook;

  ngOnInit() {
    //TODO: display book from server (with latest info) instead of taking from grid
    this.name = new FormControl(this.phoneBook.name, Validators.required);
    this.bookForm = new FormGroup({
      name: this.name
    });
  }

  saveBook(formValues) {
    this.errorMessage = "";

    this.http.put(this.baseUrl + `api/phonebooks/${this.phoneBook.id}`, formValues).subscribe(result => {
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
