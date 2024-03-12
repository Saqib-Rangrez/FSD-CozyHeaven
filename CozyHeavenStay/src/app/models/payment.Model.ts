import { Refund } from './refund.Model';
import { Booking } from './booking.Model';

export class Payment {
  paymentId: number;
  bookingId: number;
  refundId?: number | null;
  paymentMode: string;
  status: string;
  amount: number;
  paymentDate: Date;
  refund?: Refund;
  booking?: Booking;
  
  constructor(
    paymentId: number,
    bookingId: number,
    paymentMode: string,
    status: string,
    amount: number,
    paymentDate: Date,
    refundId?: number | null,
    refund?: Refund,
    booking?: Booking
  ) {
    this.paymentId = paymentId;
    this.bookingId = bookingId;
    this.refundId = refundId;
    this.paymentMode = paymentMode;
    this.status = status;
    this.amount = amount;
    this.paymentDate = paymentDate;
    this.refund = refund;
    this.booking = booking;
  }
}
