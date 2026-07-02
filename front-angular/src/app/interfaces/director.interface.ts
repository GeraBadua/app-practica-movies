export interface Director {
  id?: number; // El '?' significa que es opcional (no lo tenemos cuando creamos uno nuevo)
  name: string;
  nationality: string;
  age: number;
  active: boolean;
}