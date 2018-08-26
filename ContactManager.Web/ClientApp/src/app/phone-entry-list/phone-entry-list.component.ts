import { Component, OnInit, Inject, OnDestroy } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { BsModalService } from 'ngx-bootstrap/modal';
import { CreatePhoneEntryComponent } from "../create-phone-entry/create-phone-entry.component";
import { EditPhoneEntryComponent } from "../edit-phone-entry/edit-phone-entry.component";
import { RemovePhoneEntryComponent } from "../remove-phone-entry/remove-phone-entry.component";
import { IPhoneEntry } from '../models/phoneEntry.model';
import { IPhoneBook } from '../models/phoneBook.model';
import { Subscription } from 'rxjs';
import { CONSTANT_RELOAD_PHONE_ENTRIES } from "../shared/constants";

@Component({
  selector: 'app-phone-entry-list',
  templateUrl: './phone-entry-list.component.html',
  styleUrls: ['./phone-entry-list.component.css']
})
export class PhoneEntryListComponent implements OnInit, OnDestroy {
  phoneEntries: IPhoneEntry[];
  phoneBook: IPhoneBook;
  searchTerm: string;
  subscription: Subscription;

  constructor(private readonly http: HttpClient, @Inject('BASE_URL') private baseUrl: string,
    private readonly modalService: BsModalService, private readonly route: ActivatedRoute) {
    const phoneBookId: number = +this.route.snapshot.params['phoneBookId'];
      this.http.get<IPhoneBook>(this.baseUrl + `api/phonebooks/${phoneBookId}`).subscribe(result => {
      this.phoneBook = result;
      this.loadEntries(phoneBookId);
    }, error => console.error(error));
  }

  ngOnInit() {
    this.subscription = this.modalService.onHide.subscribe((message: string) => {
      if (message === CONSTANT_RELOAD_PHONE_ENTRIES) this.loadEntries(this.phoneBook.id);
    });
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }

  loadEntries(phoneBookId: number) {
    this.http.get<IPhoneEntry[]>(this.baseUrl + `api/phoneentries/getEntries/${phoneBookId}`).subscribe(result => {
      this.phoneEntries = result;
    }, error => console.error(error));
  }

  getEntriesToDisplay() {
    if (!this.searchTerm) {
      return this.phoneEntries;
    }
    return this.phoneEntries.filter(p => p.name.toLowerCase().includes(this.searchTerm.toLowerCase())
      || p.phoneNumber.toLowerCase().includes(this.searchTerm.toLowerCase()));
  }

  openCreateModal() {
    const initialState = {
      phoneBookId: this.phoneBook.id
    };

   this.modalService.show(CreatePhoneEntryComponent, { initialState });
  }

  openEditModal(phoneEntry: IPhoneEntry) {
    const initialState = {
      phoneEntry: phoneEntry
    };
    this.modalService.show(EditPhoneEntryComponent, { initialState });
  }

  openRemoveModal(phoneEntry: IPhoneEntry) {
    const initialState = {
      phoneEntry: phoneEntry,
      phoneBookId: this.phoneBook.id
    };
    this.modalService.show(RemovePhoneEntryComponent, { initialState });
  }
}


