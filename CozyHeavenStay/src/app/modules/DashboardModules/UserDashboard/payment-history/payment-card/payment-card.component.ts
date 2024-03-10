import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-payment-card',
  templateUrl: './payment-card.component.html',
  styleUrl: './payment-card.component.css'
})
export class PaymentCardComponent {
  @Input() data : any;
  ngDoCheck(){
    console.log(this.data)
  }
}
