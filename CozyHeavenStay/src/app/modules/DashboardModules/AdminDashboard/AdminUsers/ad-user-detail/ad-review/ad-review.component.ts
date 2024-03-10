import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-ad-review',
  templateUrl: './ad-review.component.html',
  styleUrl: './ad-review.component.css'
})
export class AdReviewComponent {
@Input() data;

ngOnInit() {
  console.log(this.data);
}
}
