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

  setToken(token: string ){
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        'Authorization': `Bearer ${token}` 
      })
    };
    return httpOptions;
  }

  getAllRooms(token: string): Observable<Room[]> { 
    return this.http.get<Room[]>(roomEndpoints.GET_ALL_ROOMS_API,this.setToken(token))
      .pipe(
        catchError(this.handleError)
      );
  }

  getRoomById(id: number,token: string): Observable<Room> { 
    return this.http.get<Room>(`${roomEndpoints.GET_ROOM_BY_ID_API}${id}`,this.setToken(token))
      .pipe(
        catchError(this.handleError)
      );
  }

  createRoom(room: any,token: string): Observable<any> { 
    const headers = new HttpHeaders();
    headers.append('Content-Type', 'multipart/form-data');
    headers.append('Authorization', `Bearer ${token}`);
    return this.http.post<any>(roomEndpoints.CREATE_ROOM_API, room , { headers: headers })
      .pipe(
        catchError(this.handleError)
      );
  }

  updateRoom(room: Room,token: string): Observable<any> { 
    return this.http.put<any>(roomEndpoints.UPDATE_ROOM_API, room,this.setToken(token))
      .pipe(
        catchError(this.handleError)
      );
  }

  deleteRoom(id: number,token: string): Observable<any> {
    return this.http.delete<any>(`${roomEndpoints.DELETE_ROOM_API}${id}`,this.setToken(token))
      .pipe(
        catchError(this.handleError)
      );
  }

  private handleError(error: any) {
    console.error('API Error:', error);
    return throwError('Something went wrong; please try again later.');
  }
}
