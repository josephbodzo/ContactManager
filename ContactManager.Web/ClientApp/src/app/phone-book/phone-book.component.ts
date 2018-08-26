import { Component, OnInit, Inject, OnDestroy } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BsModalService } from 'ngx-bootstrap/modal';
import { CreatePhoneBookComponent } from "../create-phone-book/create-phone-book.component";
import { EditPhoneBookComponent } from "../edit-phone-book/edit-phone-book.component";
import { DeletePhoneBookComponent } from "../delete-phone-book/delete-phone-book.component";
import { IPhoneBook } from '../models/phoneBook.model';
import { Subscription } from 'rxjs';
import { CONSTANT_RELOAD_PHONE_BOOKS } from "../shared/constants";

@Component({
  selector: 'app-phone-book',
  templateUrl: './phone-book.component.html',
  styleUrls: ['./phone-book.component.css']
})
export class PhoneBookComponent implements OnInit, OnDestroy {
  phoneBooks: IPhoneBook[];
  searchTerm: string;
  subscription: Subscription;

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string, private modalService: BsModalService) {
    this.loadBooks();
  }

  ngOnInit() {
    this.subscription = this.modalService.onHide.subscribe((message: string) => {
      if (message === CONSTANT_RELOAD_PHONE_BOOKS) this.loadBooks();
    });
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
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
   this.modalService.show(CreatePhoneBookComponent);
  }

  openEditModal(phoneBook: IPhoneBook) {
    const initialState = {
      phoneBook: phoneBook
    };
    this.modalService.show(EditPhoneBookComponent, {initialState});
  }

  openDeleteModal(phoneBook: IPhoneBook) {
    const initialState = {
      phoneBook: phoneBook
    };
    this.modalService.show(DeletePhoneBookComponent, { initialState });
  }
}

