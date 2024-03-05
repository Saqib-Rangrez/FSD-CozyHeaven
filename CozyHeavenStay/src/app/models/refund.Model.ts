import { Payment } from "./payment.Model";

export class Refund {
    refundId: number;
    paymentId: number;
    refundAmount: number;
    refundDate: Date;
    reason?: string;
    refundStatus?: string;
    payment?: Payment;
    
    constructor(
      refundId: number,
      paymentId: number,
      refundAmount: number,
      refundDate: Date,
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
  