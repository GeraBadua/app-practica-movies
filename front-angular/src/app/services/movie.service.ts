import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Movie } from '../interfaces/movie.interface';
import { environment } from '../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class MovieService {
  private apiUrl = `${environment.apiUrl}/movies`;

  constructor(private http: HttpClient) {}

  getMovies(): Observable<Movie[]> {
    return this.http.get<Movie[]>(this.apiUrl);
  }

  getMovie(id: number): Observable<Movie> {
    return this.http.get<Movie>(`${this.apiUrl}/${id}`);
  }

  saveMovie(movie: Movie): Observable<Movie> {
    return this.http.post<Movie>(this.apiUrl, movie);
  }

  updateMovie(id: number, movie: Movie): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/${id}`, movie);
  }

  deleteMovie(id: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/${id}`);
  }
}
