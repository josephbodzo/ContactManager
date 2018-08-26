import { Component,  Inject } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap/modal/bs-modal-ref.service';
import { HttpClient } from '@angular/common/http';
import { IPhoneBook } from '../models/phoneBook.model';

@Component({
  selector: 'app-delete-phone-book',
  templateUrl: './delete-phone-book.component.html',
  styleUrls: ['./delete-phone-book.component.css']
})
export class DeletePhoneBookComponent {
  constructor(public bsModalRef: BsModalRef, private http: HttpClient, @Inject('BASE_URL') private baseUrl: string, ) { }
  deleteSuccessful: boolean;
  errorMessage: string;
  phoneBook: IPhoneBook;

  deleteBook(id) {
    this.errorMessage = "";

    this.http.delete(this.baseUrl + `api/phonebooks/${id}`).subscribe(result => {
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
