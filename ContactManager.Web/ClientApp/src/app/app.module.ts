import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { ModalModule } from 'ngx-bootstrap/modal';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { PhoneBookComponent } from './phone-book/phone-book.component';
import { CreatePhoneBookComponent } from './create-phone-book/create-phone-book.component';
import { EditPhoneBookComponent } from './edit-phone-book/edit-phone-book.component';
import { DeletePhoneBookComponent } from './delete-phone-book/delete-phone-book.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    PhoneBookComponent,
    CreatePhoneBookComponent,
    EditPhoneBookComponent,
    DeletePhoneBookComponent
  ],
  entryComponents: [CreatePhoneBookComponent, EditPhoneBookComponent, DeletePhoneBookComponent],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule.forRoot([
      { path: '', component: PhoneBookComponent, pathMatch: 'full' },

    ]),
    ModalModule.forRoot()
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
