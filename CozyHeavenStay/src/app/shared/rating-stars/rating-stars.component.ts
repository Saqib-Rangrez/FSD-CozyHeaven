import { Component, SimpleChanges,Input } from '@angular/core';

@Component({
  selector: 'app-rating-stars',
  templateUrl: './rating-stars.component.html',
  styleUrl: './rating-stars.component.css'
})
export class RatingStarsComponent {
  @Input() reviewCount: number = 0;
  @Input() starSize: number = 20;

  fullStars: any[] = [];
  halfStars: any[] = [];
  emptyStars: any[] = [];

  ngOnChanges(changes: SimpleChanges): void {
    this.calculateStarCounts();
  }

  private calculateStarCounts(): void {
    const wholeStars = Math.floor(this.reviewCount) || 0;
    const isHalfStar = !Number.isInteger(this.reviewCount);

    this.fullStars = Array(wholeStars).fill('');
    this.halfStars = isHalfStar ? [''] : [];
    this.emptyStars = Array(isHalfStar ? 4 - wholeStars : 5 - wholeStars).fill('');
  }
}
