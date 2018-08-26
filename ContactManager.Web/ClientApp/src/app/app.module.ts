import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { ModalModule } from 'ngx-bootstrap/modal';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { PhoneBookListComponent } from './phone-book-list/phone-book-list.component';
import { CreatePhoneBookComponent } from './create-phone-book/create-phone-book.component';
import { EditPhoneBookComponent } from './edit-phone-book/edit-phone-book.component';
import { DeletePhoneBookComponent } from './delete-phone-book/delete-phone-book.component';
import { PhoneEntryListComponent } from './phone-entry-list/phone-entry-list.component';
import { CreatePhoneEntryComponent } from './create-phone-entry/create-phone-entry.component';
import { EditPhoneEntryComponent } from './edit-phone-entry/edit-phone-entry.component';
import { RemovePhoneEntryComponent } from './remove-phone-entry/remove-phone-entry.component';
import { ConfirmActionComponent } from './confirm-action/confirm-action.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    PhoneBookListComponent,
    CreatePhoneBookComponent,
    EditPhoneBookComponent,
    DeletePhoneBookComponent,
    PhoneEntryListComponent,
    CreatePhoneEntryComponent,
    EditPhoneEntryComponent,
    RemovePhoneEntryComponent,
    ConfirmActionComponent
  ],
  entryComponents: [
    CreatePhoneBookComponent,
    EditPhoneBookComponent,
    DeletePhoneBookComponent,
    RemovePhoneEntryComponent,
    EditPhoneEntryComponent,
    CreatePhoneEntryComponent,
    ConfirmActionComponent
    ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule.forRoot([
      { path: 'phoneEntries/:phoneBookId', component: PhoneEntryListComponent },
      { path: '', component: PhoneBookListComponent, pathMatch: 'full' }
    ]),
    ModalModule.forRoot()
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
