import { Payment } from "./payment.Model";

export class Refund {
  paymentId: number;
  refundAmount: number;
  refundDate: Date;
  refundId: number;
  reason?: string;
  refundStatus?: string;
  payment?: Payment;
    
    constructor(
      paymentId: number,
      refundAmount: number,
      refundDate: Date,
      refundId: number,
      reason?: string,
      refundStatus?: string,
      payment?: Payment 
    ) {
      this.refundId = refundId;
      this.paymentId = paymentId;
      this.refundAmount = refundAmount;
      this.refundDate = refundDate;
      this.reason = reason;
      this.refundStatus = refundStatus;
      this.payment = payment;
    }
  }
  