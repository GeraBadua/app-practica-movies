import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../environments/environment';
import { Director } from '../interfaces/director.interface';

@Injectable({
  providedIn: 'root'
})
export class DirectorService {
  private apiUrl = `${environment.apiUrl}/directors`;

  constructor(private http: HttpClient) {}

  getDirectors(): Observable<Director[]> {
    return this.http.get<Director[]>(this.apiUrl);
  }

  getDirector(id: number): Observable<Director> {
    return this.http.get<Director>(`${this.apiUrl}/${id}`);
  }

  saveDirector(director: Director): Observable<Director> {
    return this.http.post<Director>(this.apiUrl, director);
  }

  updateDirector(id: number, director: Director): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/${id}`, director);
  }

  deleteDirector(id: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/${id}`);
  }
}
