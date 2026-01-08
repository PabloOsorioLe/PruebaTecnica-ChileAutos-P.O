# PruebaTecnica-ChileAutos-P.O

Este proyecto consiste en una aplicacion web para explorar episodios de Rick & Morty, utilizando una arquitectura de Backend for Frontend (BFF) con .NET 8 y un frontend moderno con Angular 19.

Arquitectura y Tecnologias
Frontend: Angular 19 (Standalone Components, Signals, New Control Flow).

BFF (Backend): .NET 8 Web API.

Comunicacion: HttpClient con Tipado Estricto (Interfaces).

Estilos: CSS Puro (Sin frameworks externos por requisito).

Proceso de Desarrollo y Decisiones Tecnicas
1. Migracion a Angular 19
Se decidio crear el proyecto desde cero utilizando la version 19 para aprovechar las ultimas funcionalidades y asegurar la eliminacion de codigo muerto o dependencias innecesarias de proyectos anteriores.

Comando: ng new 02-Frontend --standalone --style=css --routing.

2. Implementacion de Signals y Control Flow
Se reemplazo el manejo de estado tradicional por Signals para una deteccion de cambios mas eficiente y se utilizo la nueva sintaxis de Control Flow (@if, @for, @empty) para mejorar la legibilidad del template.

3. Tipado Estricto (No "any")
Siguiendo los requisitos de la prueba, se elimino por completo el uso de any. Se crearon interfaces especificas para la respuesta de la API de Rick & Morty:

Episode

EpisodeResponse (manejo de paginacion e info)

4. Configuracion de Entornos
Se habilitaron los archivos de entorno para separar las URLs de desarrollo local y produccion.

Comando: ng generate environments.

Solucion de Desafios (Troubleshooting)
Error NG0908: Zone.js Missing
Al utilizar la nueva arquitectura de Angular 19 con el constructor de aplicaciones de Vite, se presento un error de falta de Zone.js.

Solucion: Se instalo la dependencia manualmente (npm install zone.js) y se configuro en la seccion de polyfills del archivo angular.json para asegurar la correcta deteccion de cambios.

Diferencias de Version (Archivos Cortos)
Se identifico que Angular 19 simplifica los nombres de archivos (ej: app.ts en lugar de app.component.ts). Se ajustaron las rutas y los imports en main.ts y app.routes.ts para reflejar esta nueva estructura.

Como ejecutar el proyecto
Requisitos
Node.js (v18+)

Angular CLI v19

.NET 8 SDK

Backend (BFF)
Navegar a la carpeta 01-Backend.

Ejecutar el comando dotnet run o iniciar desde Visual Studio.

Frontend
Navegar a la carpeta 02-Frontend.

Ejecutar npm install.

Ejecutar ng serve -o.