import { Director } from './director.interface';

export interface Movie {
  id?: number;
  name: string;
  releaseYear: number;
  genre: string;
  duration: number;
  directorId: number; // Esto es lo único que mandamos en el POST
  director?: Director; // Esto viene incluido cuando hacemos un GET gracias al .Include() de C#
}