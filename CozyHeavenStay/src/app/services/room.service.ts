import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { Room } from '../models/room.Model';
import { roomEndpoints } from './apis'; // Assuming you have a file containing API endpoints

@Injectable({
  providedIn: 'root'
})
export class RoomService {

  
  constructor(private http: HttpClient) { }

  getAllRooms(): Observable<Room[]> { 
    return this.http.get<Room[]>(roomEndpoints.GET_ALL_ROOMS_API)
      .pipe(
        catchError(this.handleError)
      );
  }

  getRoomById(id: number): Observable<Room> { 
    return this.http.get<Room>(`${roomEndpoints.GET_ROOM_BY_ID_API}${id}`)
      .pipe(
        catchError(this.handleError)
      );
  }

  createRoom(room: any): Observable<any> { 
    const headers = new HttpHeaders();
    headers.append('Content-Type', 'multipart/form-data');
    return this.http.post<any>(roomEndpoints.CREATE_ROOM_API, room , { headers: headers })
      .pipe(
        catchError(this.handleError)
      );
  }

  updateRoom(room: Room): Observable<any> { 
    return this.http.put<any>(roomEndpoints.UPDATE_ROOM_API, room)
      .pipe(
        catchError(this.handleError)
      );
  }

  deleteRoom(id: number): Observable<any> {
    return this.http.delete<any>(`${roomEndpoints.DELETE_ROOM_API}${id}`)
      .pipe(
        catchError(this.handleError)
      );
  }

  private handleError(error: any) {
    console.error('API Error:', error);
    return throwError('Something went wrong; please try again later.');
  }
}
