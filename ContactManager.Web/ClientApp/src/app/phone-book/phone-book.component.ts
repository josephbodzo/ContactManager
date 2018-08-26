import { Component, OnInit, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BsModalService } from 'ngx-bootstrap/modal';
import { BsModalRef } from 'ngx-bootstrap/modal/bs-modal-ref.service';
import { CreatePhoneBookComponent } from "../create-phone-book/create-phone-book.component";
import { EditPhoneBookComponent } from "../edit-phone-book/edit-phone-book.component";
import { DeletePhoneBookComponent } from "../delete-phone-book/delete-phone-book.component";
import { IPhoneBook } from '../models/phoneBook.model';


@Component({
  selector: 'app-phone-book',
  templateUrl: './phone-book.component.html',
  styleUrls: ['./phone-book.component.css']
})
export class PhoneBookComponent implements OnInit {
  phoneBooks: IPhoneBook[];
  searchTerm: string;
  bsModalRef: BsModalRef;

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string, private modalService: BsModalService) {
    this.loadBooks();
  }

  ngOnInit() {
    this.modalService.onHide.subscribe((reason: string) => {
      this.loadBooks();
    });
  }

  loadBooks() {
    this.http.get<IPhoneBook[]>(this.baseUrl + 'api/phonebooks').subscribe(result => {
      this.phoneBooks = result;
    }, error => console.error(error));
  }

  getBooksToDisplay() {
    if (!this.searchTerm) {
      return this.phoneBooks;
    }
    return this.phoneBooks.filter(p => p.name.toLowerCase().includes(this.searchTerm.toLowerCase()));
  }

  openCreateModal() {
    this.bsModalRef = this.modalService.show(CreatePhoneBookComponent);
  }

  openEditModal(phoneBook: IPhoneBook) {
    const initialState = {
      phoneBook: phoneBook
    };
    this.bsModalRef = this.modalService.show(EditPhoneBookComponent, {initialState});
  }

  openDeleteModal(phoneBook: IPhoneBook) {
    const initialState = {
      phoneBook: phoneBook
    };
    this.bsModalRef = this.modalService.show(DeletePhoneBookComponent, { initialState });
  }
}

