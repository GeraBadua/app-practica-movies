# Frontend - Angular 22

Frontend de la app de películas hecho con Angular. Usa Vite para build y Vitest para pruebas.

## Comandos

```bash
# Instalar dependencias
npm install

# Servidor de desarrollo
ng serve
# Abre http://localhost:4200/

# Build de producción
ng build

# Tests unitarios (Vitest)
ng test

# Generar componente/pipes/etc
ng generate component components/movie-card
```

## Estructura

```
src/
├── index.html
├── main.ts                  # entry point
├── styles.css               # global styles (TailwindCSS)
└── app/
    ├── app.ts               # root component
    ├── app.html             # template
    ├── app.css              # estilos del root
    ├── app.config.ts        # config de Angular
    ├── app.routes.ts        # rutas
    └── app.spec.ts          # test del root
```

## Tests

```bash
ng test
```
