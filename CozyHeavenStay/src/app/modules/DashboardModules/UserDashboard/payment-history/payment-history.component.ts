import { Component, inject } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { UserService } from '../../../../services/operations/user.service';
import { PaymentService } from '../../../../services/payment.service';

@Component({
  selector: 'app-payment-history',
  templateUrl: './payment-history.component.html',
  styleUrl: './payment-history.component.css'
})
export class PaymentHistoryComponent {
  paymentRecords;
  user : any;
  toastr : ToastrService = inject(ToastrService);
  paymentService : PaymentService = inject(PaymentService);
  loading : boolean;


  ngOnInit() {
    this.user = JSON.parse(localStorage.getItem('user'));
    this.loading = true;
    this.paymentService.getPaymentById(this.user.userId,this.user.token).subscribe({
      next : res => {
        this.paymentRecords = res?.data;
        this.toastr.success("Data Fetched Successfully");
      },
      error : err => {
        console.log(err);
        this.toastr.success("No payment records found");
      },
      complete : () => {
        this.loading = false;
      }
    })
  }
}
