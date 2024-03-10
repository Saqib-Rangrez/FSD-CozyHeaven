import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { Review } from '../models/review.Model';
import { reviewEndpoints } from './apis';

@Injectable({
  providedIn: 'root'
})
export class ReviewService {

  constructor(private http: HttpClient) { }
  
  setToken(token: string ){
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        'Authorization': `Bearer ${token}` 
      })
    };
    return httpOptions;
  }

  getAllReviews(token: string): Observable<Review[]> {
    return this.http.get<Review[]>(reviewEndpoints.GET_ALL_REVIEWS_API,this.setToken(token))
      .pipe(
        catchError(this.handleError)
      );
  }

  getReviewById(id: number,token: string): Observable<Review> {
    return this.http.get<Review>(`${reviewEndpoints.GET_REVIEW_BY_REVIEWID_API}${id}`,this.setToken(token))
      .pipe(
        catchError(this.handleError)
      );
  }

  getReviewByUserId(userId: number,token: string): Observable<any> {
    return this.http.get<any>(`${reviewEndpoints.GET_REVIEW_BY_USERID_API}${userId}`,this.setToken(token))
      .pipe(
        catchError(this.handleError)
      );
  }

  getReviewByHotelId(hotelId: number,token: string): Observable<Review[]> {
    return this.http.get<Review[]>(`${reviewEndpoints.GET_REVIEW_BY_HOTELID_API}${hotelId}`,this.setToken(token))
      .pipe(
        catchError(this.handleError)
      );
  }

  addReview(review: Review,token: string): Observable<Review> {
    return this.http.post<Review>(reviewEndpoints.ADD_REVIEW_API, review,this.setToken(token))
      .pipe(
        catchError(this.handleError)
      );
  }

  updateReview(review: Review,token: string): Observable<any> {
    return this.http.put<any>(reviewEndpoints.UPDATE_REVIEW_API, review,this.setToken(token))
      .pipe(
        catchError(this.handleError)
      );
  }

  deleteReview(id: number,token: string): Observable<any> {
    return this.http.delete<any>(`${reviewEndpoints.DELETE_REVIEW_API}${id}`,this.setToken(token))
      .pipe(
        catchError(this.handleError)
      );
  }

  private handleError(error: any) {
    console.error('API Error:', error);
    return throwError('Something went wrong; please try again later.');
  }
}
