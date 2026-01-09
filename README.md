## PruebaTecnica-ChileAutos-P.O
Este proyecto consiste en una aplicacion web para explorar episodios de Rick & Morty, utilizando una arquitectura de Backend for Frontend (BFF) con .NET 8 y un frontend moderno con Angular 19.

Arquitectura y Tecnologias
Frontend: Angular 19 (Standalone Components, Signals, New Control Flow).

BFF (Backend): .NET 8 Web API.

Comunicacion: HttpClient con Tipado Estricto (Interfaces).

Estilos: CSS Puro (Sin frameworks externos).

Despliegue: Render (Backend) y Vercel (Frontend).

Proceso de Desarrollo y Decisiones Tecnicas
** 1. Migracion a Angular 19 Se decidio crear el proyecto desde cero utilizando la version 19 para aprovechar las ultimas funcionalidades y asegurar la eliminacion de codigo muerto o dependencias innecesarias de proyectos anteriores.

** 2. Implementacion de Signals y Control Flow Se reemplazo el manejo de estado tradicional por Signals para una deteccion de cambios mas eficiente y se utilizo la nueva sintaxis de Control Flow (@if, @for, @empty) para mejorar la legibilidad y rendimiento del template.

** 3. Diferencias Angular 16 vs 19 A diferencia de la version 16, este proyecto utiliza el nuevo Application Builder basado en Vite y esbuild, lo que reduce drasticamente los tiempos de compilacion. Se eliminaron los NgModules en favor de Standalone Components de forma nativa y se simplifico la estructura de archivos (ej: app.ts en lugar de app.component.ts).

** 4. Tipado Estricto (No "any") Se crearon interfaces especificas para cada respuesta de la API, garantizando que el flujo de datos sea seguro y facil de mantener.

** 5. Configuracion de Entornos Se habilitaron archivos de entorno (environment.ts y environment.prod.ts) para que la aplicacion cambie automaticamente la URL del BFF al pasar de desarrollo local a produccion en la nube.

Solucion de Desafios (Troubleshooting)
** Complicaciones con CORS en Despliegue Al desplegar en nubes distintas (Render y Vercel), el navegador bloqueo las peticiones por politicas de seguridad.

Solucion: Se configuro una ProductionPolicy en el Program.cs de .NET 8 permitiendo especificamente el origen de Vercel. Ademas, se corrigio el orden de los middlewares asegurando que app.UseCors() se ejecute inmediatamente despues de app.UseRouting() y antes de mapear los controladores.

** Error de Directorio de Salida en Vercel Angular 19 genera por defecto una subcarpeta /browser dentro de dist.

Solucion: Se ajusto la configuracion de "Output Directory" en Vercel a dist/02-Frontend/browser para que el servidor encuentre correctamente el archivo index.html.

** Error NG0908: Zone.js Missing Se instalo manualmente zone.js y se añadio a los polyfills en angular.json para dar soporte a la deteccion de cambios en esta version.

UX y Navegacion Dinamica: Implementacion de Modales
** Se opto por una arquitectura de Vista Unica con Contexto para mejorar la experiencia del usuario. En lugar de navegar a una pagina independiente para ver el detalle, se implemento un Modal interactivo con backdrop-filter: blur. Esto permite que el usuario explore los personajes de un episodio sin perder su posicion en el scroll de la lista ni su termino de busqueda actual. Para el renderizado eficiente de este elemento, se utilizo la nueva sintaxis de Control Flow (@if) de Angular 19, asegurando que el modal solo exista en el DOM cuando esta activo.

Orquestacion de Datos con RxJS (forkJoin)
** Un desafio tecnico importante fue que el endpoint de episodios de la API original solo entrega un listado de URLs para los personajes, sin sus nombres ni imagenes. Para solucionar esto sin sobrecargar el servidor, se implemento una estrategia de agregacion de datos en el cliente utilizando el operador forkJoin de RxJS:

Se capturan las URLs de los personajes del episodio seleccionado.

Se disparan multiples peticiones HTTP en paralelo para obtener los perfiles individuales de forma asincrona.

El sistema espera a que todas las peticiones se completen para mostrar la informacion completa (fotos y nombres) de una sola vez, evitando el "efecto de parpadeo" y entregando una carga de datos limpia y sincronizada.

Consistencia de Datos y Localizacion
** Se garantizo la coherencia visual en la presentacion de la informacion temporal. Utilizando el DatePipe de Angular, se estandarizo el campo "Fecha de estreno" al formato local chileno dd/MM/yyyy tanto en la lista como en el modal. Esto demuestra un manejo profesional de la localizacion y asegura que el usuario reciba la informacion de manera clara y uniforme.

Gestion de Rutas y Prioridad de Navegacion
** Se resolvio un conflicto de prioridad en el enrutamiento de la aplicacion reestructurando el archivo app.routes.ts. Al mover la ruta comodin (**) al final absoluto, se permitio que el Router identifique correctamente las rutas de navegacion internas antes de aplicar la redireccion por defecto, evitando fallos de navegacion en produccion.


## Demo en desplegada y Enlace del Proyecto
** Puedes acceder a la aplicacion desplegada y funcional a traves del siguiente enlace para realizar las pruebas de usuario directamente en el navegador:

https://prueba-tecnica-chile-autos-p-o.vercel.app/episodios

Este enlace conecta directamente con el BFF alojado en Render, permitiendo una experiencia completa de navegacion, busqueda y visualizacion de detalles de los episodios de Rick & Morty.