# Contacts Management - Angular & .NET

Aplicación web para la gestión de contactos, desarrollada con **Angular** en el frontend y **.NET 7** en el backend.

# Descripción
Este proyecto permite administrar contactos (crear, listar, buscar, actualizar y eliminar)
a través de una API REST creada en .NET y un cliente web en Angular. Actualmente utiliza un 
repositorio en memoria (InMemoryContactRepository) para el almacenamiento temporal de datos,
lo que significa que los datos se pierden al reiniciar la aplicación.


## 📂 Estructura del proyecto

```
ContactosApp/
│
├── ContactosApp.Server/         # Backend en .NET 7 (API REST)
├── contactosapp.client/         # Frontend en Angular
├── ContactosApp.sln              # Solución principal
├── .gitignore
├── .gitattributes
└── README.md
```

---

## 🚀 Tecnologías utilizadas

- **Frontend:** Angular 16, TypeScript, HTML, CSS
- **Backend:** .NET 7 Web API
- **Base de datos:** Almacenamiento temporal en memoria usando ConcurrentDictionary
- **Control de versiones:** Git & GitHub

---

## ⚙️ Requisitos previos

Asegúrate de tener instalado:

- [Node.js](https://nodejs.org/) (v18 o superior)
- [Angular CLI](https://angular.io/cli)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) o [Visual Studio Code](https://code.visualstudio.com/)
- [SDK .NET 7](https://dotnet.microsoft.com/en-us/download/dotnet/7.0)

---

## 📦 Instalación y ejecución

### 1️⃣ Clonar repositorio
```bash
git clone https://github.com/IngMaicollAndresCalderonPardo/contacts-management-angular-dotnet.git
cd contacts-management-angular-dotnet
```

### 2️⃣ Backend (.NET API)
```bash
cd ContactosApp.Server
dotnet restore
dotnet run
```
Servidor por defecto: `https://localhost:5001`

### 3️⃣ Frontend (Angular)
En otra terminal:
```bash
cd contactosapp.client
npm install
ng serve
```
Aplicación disponible en: `http://localhost:4200`

---

## 📌 Funcionalidades

- CRUD de contactos (crear, leer, actualizar, eliminar)
- Separación clara frontend-backend
- API RESTful con .NET 7
- UI desarrollada con Angular Material

---
##  Limitaciones actuales
- Los datos no se guardan en una base de datos, se mantienen solo mientras la aplicación está en ejecución.
- Al reiniciar el backend, la lista de contactos se vacía.

## 📜 Licencia
Este proyecto está bajo la licencia MIT. Puedes usarlo, modificarlo y distribuirlo libremente.

👨‍💻 Desarrollado por: MAICOLL CALDERON PARDO
📅 Fecha: Agosto 2025
