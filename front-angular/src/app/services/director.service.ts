import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../environments/environment';
import { Director } from '../interfaces/director.interface';

@Injectable({
  providedIn: 'root' // Hace que el servicio esté disponible en toda la app
})
export class DirectorService {
  private apiUrl = `${environment.apiUrl}/directors`; // http://localhost:5163/api/directors

  constructor(private http: HttpClient) {}

  // Obtener todos los directores
  getDirectors(): Observable<Director[]> {
    return this.http.get<Director[]>(this.apiUrl);
  }

  // Guardar un nuevo director
  saveDirector(director: Director): Observable<Director> {
    return this.http.post<Director>(this.apiUrl, director);
  }
}