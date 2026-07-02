import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { firstValueFrom } from 'rxjs';
import { MovieService } from './services/movie.service';
import { DirectorService } from './services/director.service';
import { Movie } from './interfaces/movie.interface';
import { Director } from './interfaces/director.interface';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './app.html',
  styleUrls: ['./app.css']
})
export class App implements OnInit {
  activeTab: 'movies' | 'directors' = 'movies';

  movies: Movie[] = [];
  directors: Director[] = [];

  newDirector: Director = { name: '', nationality: '', age: 30, active: true };
  newMovie: Movie = { name: '', releaseYear: 2026, genre: '', duration: 120, directorId: 0 };

  editingMovie: Movie | null = null;
  editingDirector: Director | null = null;

  successMessage: string | null = null;
  errorMessage: string | null = null;

  deletingMovieId: number | null = null;
  deletingDirectorId: number | null = null;

  constructor(
    private movieService: MovieService,
    private directorService: DirectorService,
    private cdr: ChangeDetectorRef
  ) {}

  ngOnInit() {
    this.loadAllData();
  }

  clearMessages() {
    this.successMessage = null;
    this.errorMessage = null;
  }

  showSuccess(msg: string) {
    this.successMessage = msg;
    setTimeout(() => this.successMessage = null, 3000);
  }

  showError(msg: string) {
    this.errorMessage = msg;
    setTimeout(() => this.errorMessage = null, 5000);
  }

  async loadAllData() {
    try {
      this.movies = await firstValueFrom(this.movieService.getMovies());
    } catch {
      this.errorMessage = 'Error al cargar peliculas';
    }
    try {
      this.directors = await firstValueFrom(this.directorService.getDirectors());
    } catch {
      this.errorMessage = 'Error al cargar directores';
    }
    this.cdr.detectChanges();
  }

  setTab(tab: 'movies' | 'directors') {
    this.activeTab = tab;
    this.clearMessages();
  }

  onEditMovie(movie: Movie) {
    this.clearMessages();
    this.editingMovie = { ...movie };
    this.newMovie = {
      id: movie.id,
      name: movie.name,
      releaseYear: movie.releaseYear,
      genre: movie.genre,
      duration: movie.duration,
      directorId: movie.directorId,
    };
  }

  onEditDirector(director: Director) {
    this.clearMessages();
    this.editingDirector = { ...director };
    this.newDirector = {
      id: director.id,
      name: director.name,
      nationality: director.nationality,
      age: director.age,
      active: director.active,
    };
  }

  cancelEdit() {
    this.editingMovie = null;
    this.editingDirector = null;
    this.newMovie = { name: '', releaseYear: 2026, genre: '', duration: 120, directorId: 0 };
    this.newDirector = { name: '', nationality: '', age: 30, active: true };
  }

  async onSubmitDirector() {
    this.clearMessages();
    if (!this.newDirector.name || !this.newDirector.nationality) return;

    if (this.editingDirector) {
      try {
        await firstValueFrom(this.directorService.updateDirector(this.editingDirector.id!, this.newDirector));
        await this.loadAllData();
        this.cancelEdit();
        this.showSuccess('Director actualizado correctamente');
      } catch (err: any) {
        this.showError(err.error?.message || 'Error al actualizar el director');
      }
    } else {
      try {
        await firstValueFrom(this.directorService.saveDirector(this.newDirector));
        await this.loadAllData();
        this.resetDirectorForm();
        this.showSuccess('Director registrado correctamente');
      } catch (err: any) {
        this.showError(err.error?.message || 'Error al guardar el director');
      }
    }
    this.cdr.detectChanges();
  }

  async onSubmitMovie() {
    this.clearMessages();
    if (!this.newMovie.name || !this.newMovie.genre || !this.newMovie.directorId) {
      this.showError('Completa todos los campos y selecciona un director');
      return;
    }

    this.newMovie.directorId = Number(this.newMovie.directorId);

    if (this.editingMovie) {
      try {
        await firstValueFrom(this.movieService.updateMovie(this.editingMovie.id!, this.newMovie));
        await this.loadAllData();
        this.cancelEdit();
        this.showSuccess('Pelicula actualizada correctamente');
      } catch (err: any) {
        this.showError(err.error?.message || 'Error al actualizar la pelicula');
      }
    } else {
      try {
        await firstValueFrom(this.movieService.saveMovie(this.newMovie));
        await this.loadAllData();
        this.resetMovieForm();
        this.showSuccess('Pelicula registrada correctamente');
      } catch (err: any) {
        this.showError(err.error?.message || 'Error al guardar la pelicula');
      }
    }
    this.cdr.detectChanges();
  }

  async onDeleteMovie(id?: number) {
    this.clearMessages();
    if (!id || this.deletingMovieId === id) return;
    this.deletingMovieId = id;

    try {
      await firstValueFrom(this.movieService.deleteMovie(id));
      this.movies = this.movies.filter(m => m.id !== id);
      this.showSuccess('Pelicula eliminada');
    } catch (err: any) {
      this.showError(err.error?.message || 'Error al eliminar la pelicula');
    } finally {
      this.deletingMovieId = null;
    }
    this.cdr.detectChanges();
  }

  async onDeleteDirector(id?: number) {
    this.clearMessages();
    if (!id || this.deletingDirectorId === id) return;
    this.deletingDirectorId = id;

    try {
      await firstValueFrom(this.directorService.deleteDirector(id));
      this.directors = this.directors.filter(d => d.id !== id);
      this.showSuccess('Director eliminado');
    } catch (err: any) {
      this.showError(err.error?.message || 'Error al eliminar el director');
    } finally {
      this.deletingDirectorId = null;
    }
    this.cdr.detectChanges();
  }

  private resetDirectorForm() {
    this.newDirector = { name: '', nationality: '', age: 30, active: true };
  }

  private resetMovieForm() {
    this.newMovie = { name: '', releaseYear: 2026, genre: '', duration: 120, directorId: 0 };
  }
}
