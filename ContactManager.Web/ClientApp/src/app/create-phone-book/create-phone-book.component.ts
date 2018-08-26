import { Component, OnInit, Inject } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap/modal/bs-modal-ref.service';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-create-phone-book',
  templateUrl: './create-phone-book.component.html',
  styleUrls: ['./create-phone-book.component.css']
})
export class CreatePhoneBookComponent implements OnInit {
  constructor(public bsModalRef: BsModalRef, private http: HttpClient, @Inject('BASE_URL') private baseUrl: string,) { }
  name: FormControl;
  bookForm: FormGroup;
  saveSuccessful: boolean;
  errorMessage: string;

  ngOnInit() {
    this.name = new FormControl(null, Validators.required);
    this.bookForm = new FormGroup({
      name: this.name
    });
  }

  saveBook(formValues) {
    this.errorMessage = "";
    this.http.post(this.baseUrl + 'api/phonebooks', formValues).subscribe(result => {
        this.saveSuccessful = true;
        setTimeout(() => this.bsModalRef.hide(), 1000);
      },
      error => {
        if (error.error && !error.error.includes("DOCTYPE"))
          this.errorMessage = error.error;
        else
          this.errorMessage = "Oops! An error occurred whilst processing your request.";
      });
  }
}
