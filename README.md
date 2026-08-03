# Crop Advisory System (CAS)

**AI-assisted Crop Advisory Platform for Farmers**

CAS is a web-based agricultural advisory system built with **ASP.NET Core MVC**.  
It helps farmers and agricultural officers get tailored crop advice based on crop type, soil type, season, and location — including watering, fertilizer, pest control, and harvesting guidance.

---

## Overview

The Crop Advisory System enables:

- Farmers to register and receive personalized crop advisories
- Admins / Agricultural Officers to manage crops, soil types, seasons, and advisory content
- Weather-aware advice logging
- Media management for crop images (Cloudinary)

### Key Features

- Farmer registration & authentication (Cookie-based)
- Role-based access (Farmer, Admin, etc.)
- Crop management (with images)
- Soil Type management
- Season management
- Crop Advisory creation (Watering, Fertilizer, Pest Control, Harvesting tips)
- Bulk advisory creation
- Weather logging linked to advisories
- Cloudinary image upload
- FluentValidation for request validation
- PostgreSQL database

---

## Tech Stack

| Layer              | Technology                              |
|--------------------|-----------------------------------------|
| Framework          | ASP.NET Core MVC (.NET 10)              |
| Database           | PostgreSQL + Entity Framework Core      |
| Authentication     | Cookie Authentication                   |
| Validation         | FluentValidation                        |
| Media Storage      | Cloudinary                              |
| Architecture       | Layered (Controllers → Services → Repositories → DbContext) |
| UI                 | Razor Views + SCSS / Bootstrap-style assets |

---

## Solution Structure

```
CAS/
├── Controllers/           # MVC Controllers (Auth, Crop, SoilType, Season, Advisory, User)
├── Models/
│   └── Entities/          # Domain entities
├── DTOs/                  # Request/Response models
├── Interfaces/
│   ├── Services/          # Service contracts
│   └── Repositories/      # Repository contracts
├── Implementation/
│   ├── Services/          # Business logic
│   └── Repositories/      # Data access
├── Identity/              # Identity & password hashing helpers
├── Contracts/             # Enums, shared contracts
├── Configuration/         # Settings (Cloudinary, etc.)
├── CASDbContext/          # EF Core DbContext
├── Views/                 # Razor views
└── wwwroot/               # Static assets (CSS, JS, images)
```

---

## Getting Started

### Prerequisites

- .NET 10 SDK
- PostgreSQL 15+
- Visual Studio 2022 / Rider / VS Code

### 1. Clone the repository

```bash
git clone https://github.com/AbassAdebayo/crop-advisory-system.git
cd crop-advisory-system
```

### 2. Configure Database

Update the connection string in `appsettings.Development.json` or `appsettings.json`:

```json
"ConnectionStrings": {
  "CASConnection": "Host=localhost;Port=5432;Database=CAS_db;Username=postgres;Password=your_password;"
}
```

### 3. Apply Migrations

```bash
cd CAS
dotnet ef database update
```

### 4. Run the Application

```bash
dotnet run --project CAS
```

Navigate to: `https://localhost:<port>`

---

## Core Domain Entities

| Entity         | Description                                      |
|----------------|--------------------------------------------------|
| `User`         | Farmers and system users                         |
| `Role`         | User roles (Farmer, Admin, etc.)                 |
| `Crop`         | Crop types with image and status                 |
| `SoilType`     | Soil classifications                             |
| `Season`       | Planting / growing seasons                       |
| `Advisory`     | Tailored advice linking Crop + Soil + Season     |
| `WeatherLog`   | Weather data linked to advisory & user           |
| `SaveGuide`    | Saved/bookmarked guides for users                |

---

## Main Modules

### Authentication
- Farmer registration
- Login / Logout (Cookie authentication)
- Role-based access control

### Crop Management
- Create, update, activate/deactivate crops
- Upload crop images via Cloudinary

### Soil Type & Season Management
- CRUD for soil types and seasons
- Status management (Active / Inactive)

### Advisory Engine
- Create single or bulk advisories
- Advice categories:
  - Watering
  - Fertilizer
  - Pest Control
  - Harvesting Tips
- Filter advisories by Crop + Soil Type + Season

### Weather Integration
- Log temperature, humidity, rain chance, wind speed
- Generate weather-based advice

---

## Documentation

- [Architecture & System Diagrams](docs/architecture.md) – System architecture, entity relationships, advisory flow, and module overview.

---

## Configuration

| Key                        | Description                     |
|---------------------------|---------------------------------|
| `ConnectionStrings:CASConnection` | PostgreSQL connection string |
| `Cloudinary`              | Cloud name, API key & secret    |

> **Security Note**: Never commit real secrets. Use User Secrets or environment variables in production.

---

## Roadmap

- [x] Farmer registration & authentication
- [x] Crop, Soil Type, Season management
- [x] Advisory creation & management
- [x] Weather logging
- [ ] Mobile-friendly responsive improvements
- [ ] Real-time weather API integration
- [ ] SMS / WhatsApp advisory notifications
- [ ] Farmer dashboard analytics
- [ ] Multi-language support

---

## Contributing

1. Create a feature branch
2. Follow existing layered architecture
3. Add FluentValidation for new requests
4. Submit a Pull Request

---

## License

Proprietary – All rights reserved.

---

**Built with** ASP.NET Core MVC, Entity Framework Core, PostgreSQL, and Cloudinary.
