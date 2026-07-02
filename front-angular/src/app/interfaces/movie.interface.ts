import { Director } from './director.interface';

export interface Movie {
  id?: number;
  name: string;
  releaseYear: number;
  genre: string;
  duration: number;
  directorId: number;
  director?: Director;
}
