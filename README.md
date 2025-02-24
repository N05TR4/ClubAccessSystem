
# ClubAccessSystem - Sistema de Gestión de Accesos para Clubes Recreativos

## 📋 Descripción del Proyecto
Sistema completo para gestionar accesos de clientes a clubes recreativos, con:
- Registro de entradas/salidas
- Gestión de usuarios y roles (Admin/Staff)
- Tipos de clientes (Visitantes/Miembros)
- Historial de accesos
- Autenticación segura JWT
- Interfaz responsive

🔗 **Repositorio:** https://github.com/N05TR4/ClubAccessSystem

---

## 🛠 Tecnologías Utilizadas

### Backend (.NET 8)
| Tecnología              | Uso                                                                 |
|-------------------------|---------------------------------------------------------------------|
| .NET 8                  | Framework principal                                                 |
| Entity Framework Core 8 | ORM para MySQL                                                     |
| xUnit                   | Pruebas unitarias                                                  |
| JWT Bearer              | Autenticación y autorización                                       |
| AutoMapper              | Mapeo entre modelos y DTOs                                         |
| Swagger                 | Documentación de API                                               |
| MySQL                   | Base de datos relacional                                           |

### Frontend (React + Vite)
| Tecnología              | Uso                                                                 |
|-------------------------|---------------------------------------------------------------------|
| React 18                | Biblioteca principal                                               |
| Vite                    | Bundler y entorno de desarrollo                                    |
| React Router 6          | Enrutamiento                                                       |
| Axios                   | Comunicación con API                                               |
| Jotai                   | Gestión de estado global                                           |
| Tailwind CSS            | Estilizado                                                         |
| React Hook Form         | Manejo de formularios                                              |
| Zod                     | Validación de datos                                                |

---

## 🏗 Arquitectura
**Backend:** Clean Architecture con separación en capas:
```
Core/          ← Entidades y interfaces
Infrastructure/← Implementaciones (EF Core, servicios)
API/           ← Controladores y configuración
Tests/         ← Pruebas unitarias
```

**Frontend:** Arquitectura modular por features:
```
src/
  ├── api/         ← Servicios API
  ├── components/  ← Componentes reutilizables
  ├── features/    ← Lógica por módulo (auth, users, entries)
  ├── hooks/       ← Custom hooks
  ├── types/       ← Tipos TypeScript
  └── utils/       ← Funciones helpers
```

---

## 🚀 Cómo Ejecutar el Proyecto

### Requisitos Previos
- .NET 8 SDK
- Node.js 18+
- MySQL 8+
- Git

### Pasos de Instalación

1. **Clonar repositorio:**
```bash
git clone https://github.com/N05TR4/ClubAccessSystem.git
cd ClubAccessSystem
```

2. **Configurar Base de Datos:**
```bash
# Crear base de datos
mysql -u root -p

CREATE DATABASE DbClubAccessSystem;
USE DbClubAccessSystem;


CREATE TABLE Roles (
	RolId INT PRIMARY KEY AUTO_INCREMENT,
    Nombre VARCHAR(100) NOT NULL,
    CreatedAt TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    UpdatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP
);

CREATE TABLE TipoClientes (
	TipoClienteId INT PRIMARY KEY AUTO_INCREMENT,
    Nombre VARCHAR(100) NOT NULL,
    CreatedAt TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    UpdatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP
);


CREATE TABLE Usuarios (
    UsuarioId INT PRIMARY KEY AUTO_INCREMENT,
    Nombre VARCHAR(100) NOT NULL,
    Email VARCHAR(100) NOT NULL UNIQUE,
    Password VARCHAR(255) NOT NULL,
    RolId INT,
    CreatedAt TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    UpdatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    FOREIGN KEY (RolId) REFERENCES Roles(RolId)
);

CREATE TABLE Clientes (
    ClienteId INT PRIMARY KEY AUTO_INCREMENT,
    Nombre VARCHAR(100) NOT NULL,
    Contacto VARCHAR(100) NOT NULL,
    TipoCliente INT,
    CreatedAt TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    UpdatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    FOREIGN KEY (TipoCliente) REFERENCES TipoClientes(TipoClienteId)
);

CREATE TABLE RegistrosAcceso (
    RegistroId INT PRIMARY KEY AUTO_INCREMENT,
    FechaEntrada DATETIME NOT NULL,
    FechaSalida DATETIME,
    ClienteId INT,
    CreatedAt TIMESTAMP DEFAULT CURRENT_TIMESTAMP ,
    UpdatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP, 
    FOREIGN KEY (ClienteId) REFERENCES Clientes(ClienteId)
    
);


-- Insertar Roles
INSERT INTO Roles (Nombre) VALUES ('Administrador');
INSERT INTO Roles (Nombre) VALUES ('Personal');

-- Insertar TipoClientes
INSERT INTO TipoClientes (Nombre) VALUES ('Visitante');
INSERT INTO TipoClientes (Nombre) VALUES ('Miembro');

-- Insertar Usuarios
INSERT INTO Usuarios (Nombre, Email, Password, RolId) 
VALUES ('Admin', 'admin@club.com', 'admin123', 1);

INSERT INTO Usuarios (Nombre, Email, Password, RolId) 
VALUES ('Personal1', 'personal1@club.com', 'personal123', 2);

-- Insertar Clientes
INSERT INTO Clientes (Nombre, Contacto, TipoCliente) 
VALUES ('Juan Perez', 'juan@example.com', 1);

INSERT INTO Clientes (Nombre, Contacto, TipoCliente) 
VALUES ('Maria Gomez', 'maria@example.com', 2);

-- Insertar RegistrosAcceso
INSERT INTO RegistrosAcceso (FechaEntrada, ClienteId) 
VALUES ('2023-10-01 10:00:00', 1);

UPDATE RegistrosAcceso 
SET FechaSalida = '2023-10-01 12:00:00' 
WHERE RegistroId = 1;

INSERT INTO RegistrosAcceso (FechaEntrada, ClienteId) 
VALUES ('2023-10-01 11:00:00', 2);


exit
```

3. **Backend (.NET):**
```bash
cd Backend/ClubAccess.API

# Restaurar dependencias
dotnet restore

# Configurar connection string en appsettings.json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=ClubAccess;User=root;Password=tu_password;"
  }
}



# Iniciar servidor
dotnet run
```

4. **Frontend (React):**
```bash
cd Frontend/club-access-system

# Instalar dependencias
npm install

# Configurar variables de entorno
cp .env.example .env
# Editar .env:
VITE_API_URL=http://localhost:5000

# Iniciar aplicación
npm run dev
```

---

## 🔑 Credenciales de Prueba
**Usuario Admin:**
```
Email: admin@club.com
Password: Admin123!
```

**Usuario Staff:**
```
Email: personal1@club.com
Password: Personal123
```

---

## 📚 Documentación API
Accede a Swagger UI después de iniciar el backend:
```
http://localhost:5000/swagger
```

---

## ✅ Features Implementados
1. **Autenticación JWT** con refresh tokens
2. CRUD completo para:
   - Usuarios (Admin)
   - Clientes (Visitantes/Miembros)
   - Registro de accesos
3. Dashboard con métricas:
   - Accesos diarios
   - Tipos de clientes
   - Historial completo
4. Sistema de roles:
   - Admin: Gestión completa
   - Staff: Solo registro de accesos
5. Validaciones en tiempo real
6. Interfaz responsive
7. 90%+ cobertura de pruebas

---

## 📝 Mejoras Futuras
- Mejora en la estructura y funcionamiento del proyecto
- Sistema de membresías con límite de visitas
- Integración con sistemas de pago
- Exportación de reportes en PDF/Excel
- Notificaciones por email
- Autenticación de dos factores

Este README.md ya está disponible en el repositorio con formato Markdown para su uso inmediato.