# 🏴‍☠ OnePieceAPI
OnePieceAPI es un proyecto personal que estoy desarrollando con ASP.NET Core para practicar la creación de APIs RESTful.  
Está inspirado en el universo de One Piece y permite gestionar piratas, frutas del diablo y sus relaciones.

## Tecnologías utilizadas
- C#
- ASP.NET Core Web API
- Entity Framework Core
- AutoMapper
- SQL Server (LocalDB)
- Swagger / Postman
- Git & GitHub

## 🚀 ¿Cómo ejecutar el proyecto?

1. **Clonar el repositorio**
   ```bash
   git clone https://github.com/FrancisFe/OnePieceAPI.git
   cd OnePieceAPI
   ```

2. **Restaurar paquetes NuGet:**
   ```bash
   dotnet restore
   ```

3. **Aplicar migraciones y crear la base de datos** (Configura la cadena de conexión en `appsettings.json`):
   ```bash
   dotnet ef database update
   ```

4. **Ejecuta la API:**
   ```bash
   dotnet run
   ```

5. **Abrí swagger en el navegador:**
   ```
   http://localhost:{PORT}/swagger
   ```

## 📌 Endpoints disponibles
A continuación se listan los principales endpoints de la API:

### 🏴‍☠️ Piratas
| Método | Ruta                  | Descripción                        |
|--------|-----------------------|------------------------------------|
| GET    | /api/piratas          | Obtener todos los piratas          |
| GET    | /api/piratas/{id}     | Obtener un pirata por ID           |
| POST   | /api/piratas          | Crear un nuevo pirata              |
| PUT    | /api/piratas/{id}     | Actualizar un pirata existente     |
| DELETE | /api/piratas/{id}     | Eliminar un pirata                 |

### 🍇 Frutas del Diablo
| Método | Ruta                            | Descripción                             |
|--------|----------------------------------|------------------------------------------|
| GET    | /api/frutasdeldiablo            | Obtener todas las frutas del diablo     |
| GET    | /api/frutasdeldiablo/{id}       | Obtener una fruta por ID                |
| POST   | /api/frutasdeldiablo            | Crear una nueva fruta                   |
| PUT    | /api/frutasdeldiablo/{id}       | Actualizar una fruta existente          |
| DELETE | /api/frutasdeldiablo/{id}       | Eliminar una fruta                      |