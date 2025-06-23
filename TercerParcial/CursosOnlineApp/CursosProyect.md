# CursosOnlineApp

Proyecto de cursos en línea desarrollado en **.NET** con integración de **Tailwind CSS**.

## Requisitos

- [.NET 7 SDK](https://dotnet.microsoft.com/download)
- [Node.js](https://nodejs.org/)
- [Tailwind CSS](https://tailwindcss.com/docs/installation)
- [Visual Studio](https://visualstudio.microsoft.com/) o [Rider](https://www.jetbrains.com/rider/)

## Instalación

1. **Clona el repositorio:**
    ```bash
    git clone https://github.com/bvazquezj/dotnet2021602462/CursosOnlineApp.git
    cd CursosOnlineApp
    ```

2. **Restaura dependencias .NET:**
    ```bash
    dotnet restore
    ```

3. **Instala Tailwind CSS:**
    ```bash
    npm install -D tailwindcss postcss autoprefixer
    npx tailwindcss init -p
    ```

4. **Configura `tailwind.config.js`:**
    ```js
    module.exports = {
      content: [
         "./**/*.cshtml",
         "./wwwroot/js/**/*.js"
      ],
      theme: {
         extend: {},
      },
      plugins: [],
    }
    ```

5. **Agrega Tailwind a tu CSS principal (`wwwroot/css/site.css`):**
    ```css
    @tailwind base;
    @tailwind components;
    @tailwind utilities;
    ```

6. **Compila Tailwind:**
    ```bash
    npx tailwindcss -i ./wwwroot/css/site.css -o ./wwwroot/css/output.css --watch
    ```

## Ejecución

```bash
dotnet run
```

Abre tu navegador en `https://localhost:5001` o la URL indicada en la consola.

## Estructura del Proyecto

- `/Controllers` — Controladores MVC
- `/Models` — Modelos de datos
- `/Views` — Vistas Razor
- `/wwwroot` — Archivos estáticos (CSS, JS, imágenes)

## Recursos

- [Documentación .NET](https://learn.microsoft.com/dotnet/)
- [Guía Tailwind CSS](https://tailwindcss.com/docs/installation)

---

¡Listo para crear tu plataforma de cursos online con .NET y Tailwind CSS!
