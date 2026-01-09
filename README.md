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


** UX y Navegacion Dinamica: Implementacion de Modales
Se opto por una arquitectura de Vista Unica con Contexto para mejorar la experiencia del usuario. En lugar de navegar a una pagina independiente para ver el detalle, se implento un Modal interactivo con backdrop-filter: blur. Esto permite que el usuario explore los personajes de un episodio sin perder su posicion en el scroll de la lista ni su termino de busqueda actual. Para el renderizado eficiente de este elemento, se utilizo la nueva sintaxis de Control Flow (@if) de Angular 19, asegurando que el modal solo exista en el DOM cuando esta activo.

** Orquestacion de Datos con RxJS (forkJoin)
Un desafio tecnico importante fue que el endpoint de episodios de la API original solo entrega un listado de URLs para los personajes, sin sus nombres ni imagenes. Para solucionar esto sin sobrecargar el servidor, se implento una estrategia de agregacion de datos en el cliente utilizando el operador forkJoin de RxJS:

Se capturan las URLs de los personajes del episodio seleccionado.

Se disparan multiples peticiones HTTP en paralelo para obtener los perfiles individuales.

El sistema espera a que todas las peticiones se completen para mostrar la informacion completa (fotos y nombres) de una sola vez, evitando el "efecto de parpadeo" y entregando una carga de datos limpia y sincronizada.

** Consistencia de Datos y Localizacion
Se garantizo la coherencia visual en la presentacion de la informacion temporal. Utilizando el DatePipe de Angular, se estandarizo el campo "Fecha de estreno" (tanto en las tarjetas de la lista como en el detalle del modal) al formato local chileno dd/MM/yyyy. Esto demuestra un manejo profesional de la localizacion y asegura que el usuario reciba la informacion de manera clara y uniforme en toda la plataforma.

** Gestion de Rutas y Prioridad de Navegacion
Durante el desarrollo, se resolvio un conflicto de prioridad en el enrutamiento de la aplicacion. Se reestructuro el archivo app.routes.ts para asegurar que las rutas especificas tengan prioridad sobre las genericas. Al mover la ruta comodin (**) al final absoluto del arreglo de rutas, se permitio que el Router de Angular identifique correctamente las rutas de navegacion internas antes de aplicar la redireccion por defecto.