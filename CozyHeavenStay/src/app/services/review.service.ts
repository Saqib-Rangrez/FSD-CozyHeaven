import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { Review } from '../models/review.Model';
import { reviewEndpoints } from './apis';

@Injectable({
  providedIn: 'root'
})
export class ReviewService {

  constructor(private http: HttpClient) { }

  getAllReviews(): Observable<Review[]> {
    return this.http.get<Review[]>(reviewEndpoints.GET_ALL_REVIEWS_API)
      .pipe(
        catchError(this.handleError)
      );
  }

  getReviewById(id: number): Observable<Review> {
    return this.http.get<Review>(`${reviewEndpoints.GET_REVIEW_BY_REVIEWID_API}${id}`)
      .pipe(
        catchError(this.handleError)
      );
  }

  getReviewByUserId(userId: number): Observable<any> {
    return this.http.get<any>(`${reviewEndpoints.GET_REVIEW_BY_USERID_API}${userId}`)
      .pipe(
        catchError(this.handleError)
      );
  }

  getReviewByHotelId(hotelId: number): Observable<Review[]> {
    return this.http.get<Review[]>(`${reviewEndpoints.GET_REVIEW_BY_HOTELID_API}${hotelId}`)
      .pipe(
        catchError(this.handleError)
      );
  }

  addReview(review: Review): Observable<Review> {
    return this.http.post<Review>(reviewEndpoints.ADD_REVIEW_API, review)
      .pipe(
        catchError(this.handleError)
      );
  }

  updateReview(review: Review): Observable<any> {
    return this.http.put<any>(reviewEndpoints.UPDATE_REVIEW_API, review)
      .pipe(
        catchError(this.handleError)
      );
  }

  deleteReview(id: number): Observable<any> {
    return this.http.delete<any>(`${reviewEndpoints.DELETE_REVIEW_API}${id}`)
      .pipe(
        catchError(this.handleError)
      );
  }

  private handleError(error: any) {
    console.error('API Error:', error);
    return throwError('Something went wrong; please try again later.');
  }
}
