# Frontend - Angular 22

SPA para el sistema de gestion de peliculas con Angular 22 y Tailwind CSS 4.

## Requisitos

- Node.js >= 22
- Backend corriendo en http://localhost:5163

## Ejecucion

```bash
npm install
ng serve
```

La app corre en http://localhost:4200/.

Si el backend corre en otro puerto, ajusta `src/app/environments/environment.ts`:

```ts
export const environment = {
  production: false,
  apiUrl: 'http://localhost:5163/api'
};
```

## Tests

```bash
ng test
```

Usa Vitest. La configuracion esta en `angular.json`.
