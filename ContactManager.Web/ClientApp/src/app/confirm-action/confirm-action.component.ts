import { Component, OnInit } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap/modal/bs-modal-ref.service';
import { BsModalService } from 'ngx-bootstrap/modal';
import { EnumConfirmationResult } from "../shared/enums/confirmation-result.enum";

@Component({
  selector: 'app-confirm-action',
  templateUrl: './confirm-action.component.html',
  styleUrls: ['./confirm-action.component.css']
})
export class ConfirmActionComponent implements OnInit {
  message: string = "Are you sure you want to perform this action?";
  constructor(public bsModalRef: BsModalRef, private modalService: BsModalService) { }

  ngOnInit() {
  }

  closeDialog(isConfirmed: boolean) {
    const result = isConfirmed
      ? EnumConfirmationResult[EnumConfirmationResult.Confirmed]
      : EnumConfirmationResult[EnumConfirmationResult.Declined];
    this.modalService.setDismissReason(result);
    this.bsModalRef.hide();
  }
}
