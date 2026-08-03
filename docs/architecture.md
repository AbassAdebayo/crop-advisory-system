# Crop Advisory System (CAS) – Architecture & System Diagrams

This document contains the system architecture and key flow diagrams for the Crop Advisory System.

---

## 1. High-Level Architecture

```mermaid
flowchart TB
    subgraph Client["Clients"]
        Browser["Web Browser<br/>(Farmers & Admins)"]
        Mobile["Future Mobile App"]
    end

    subgraph Presentation["CAS (ASP.NET Core MVC)"]
        Controllers["Controllers<br/>Auth, Crop, SoilType,<br/>Season, Advisory, User"]
        Views["Razor Views"]
        Static["wwwroot Assets"]
    end

    subgraph Business["Business Layer"]
        Services["Services<br/>CropService, AdvisoryService,<br/>SeasonService, SoilTypeService,<br/>UserService, IdentityService"]
        Validators["FluentValidation"]
    end

    subgraph Data["Data Access Layer"]
        Repos["Repositories + UnitOfWork"]
        DbContext["CASContext (EF Core)"]
    end

    subgraph External["External Services"]
        Postgres[(PostgreSQL)]
        Cloudinary["Cloudinary<br/>(Crop Images)"]
    end

    Client --> Controllers
    Controllers --> Views
    Controllers --> Services
    Services --> Validators
    Services --> Repos
    Repos --> DbContext
    DbContext --> Postgres
    Services --> Cloudinary
```

---

## 2. Layered Architecture

```mermaid
graph TD
    A[Controllers] --> B[Services]
    B --> C[Repositories]
    C --> D[CASContext / EF Core]
    D --> E[(PostgreSQL)]

    B --> F[Cloudinary Service]
    B --> G[Identity / Password Hasher]
    A --> H[Razor Views]

    style A fill:#bfb,stroke:#333
    style B fill:#bbf,stroke:#333
    style C fill:#fbb,stroke:#333
    style D fill:#f9f,stroke:#333
```

---

## 3. Authentication Flow

```mermaid
sequenceDiagram
    participant Farmer
    participant AuthController
    participant UserService
    participant IdentityService
    participant DB

    Farmer->>AuthController: POST /Auth/RegisterFarmer
    AuthController->>UserService: RegisterFarmerAsync()
    UserService->>IdentityService: Hash password
    UserService->>DB: Create User + assign Farmer role
    DB-->>Farmer: Registration success

    Farmer->>AuthController: POST /Auth/Login
    AuthController->>UserService: Validate credentials
    UserService->>IdentityService: Verify password
    AuthController->>AuthController: SignIn (Cookie)
    AuthController-->>Farmer: Redirect to Dashboard
```

---

## 4. Core Domain Entity Relationships

```mermaid
erDiagram
    USER ||--o{ ROLE : has
    USER ||--o{ WEATHERLOG : logs
    USER ||--o{ SAVEGUIDE : saves

    CROP ||--o{ ADVISORY : has
    SOILTYPE ||--o{ ADVISORY : has
    SEASON ||--o{ ADVISORY : has

    ADVISORY ||--o{ WEATHERLOG : linked_to
    ADVISORY ||--o{ SAVEGUIDE : bookmarked_as

    USER {
        Guid Id
        string FullName
        string Email
        string PasswordHash
        string PhoneNumber
        string Location
        Guid RoleId
    }

    CROP {
        Guid Id
        string Name
        string Description
        string ImageUrl
        Status CropStatus
    }

    SOILTYPE {
        Guid Id
        string Name
        string Description
        Status SoilTypeStatus
    }

    SEASON {
        Guid Id
        string Name
        string Description
        Status SeasonStatus
    }

    ADVISORY {
        Guid Id
        Guid CropId
        Guid SoilTypeId
        Guid SeasonId
        string Title
        string WateringAdvice
        string FertilizerAdvice
        string PestControlAdvice
        string HarvestingTips
        Status AdvisoryStatus
    }
```

---

## 5. Advisory Creation Flow

```mermaid
flowchart LR
    A[Admin / Officer] --> B[Select Crop]
    B --> C[Select Soil Type]
    C --> D[Select Season]
    D --> E[Enter Advice<br/>Watering / Fertilizer<br/>Pest / Harvest]
    E --> F[Create Advisory]
    F --> G[(Database)]
    G --> H[Farmer can view<br/>filtered advisories]
```

### Advisory Sequence

```mermaid
sequenceDiagram
    participant Admin
    participant AdvisoryController
    participant AdvisoryService
    participant Repositories
    participant DB

    Admin->>AdvisoryController: GET /Advisory/Create
    AdvisoryController->>AdvisoryService: Load Crops, Seasons, SoilTypes
    AdvisoryController-->>Admin: Create form

    Admin->>AdvisoryController: POST /Advisory/Create (bulk)
    AdvisoryController->>AdvisoryService: CreateAdvisoryAsync()
    AdvisoryService->>Repositories: Validate & Save
    Repositories->>DB: Insert Advisory records
    DB-->>Admin: Success message
```

---

## 6. Farmer Advisory Lookup Flow

```mermaid
sequenceDiagram
    participant Farmer
    participant System
    participant DB

    Farmer->>System: Select Crop + Soil Type + Season
    System->>DB: Query matching Advisories
    DB-->>System: Advisory list
    System-->>Farmer: Show Watering, Fertilizer,<br/>Pest Control & Harvest tips

    opt Weather aware
        Farmer->>System: Request weather advice
        System->>DB: Create WeatherLog
        System-->>Farmer: Weather-based recommendation
    end
```

---

## 7. Module Overview

```mermaid
flowchart TB
    subgraph Core["Core"]
        Auth[Authentication & Roles]
        Users[User Management]
    end

    subgraph MasterData["Master Data"]
        Crops[Crop Management]
        Soil[Soil Type Management]
        Seasons[Season Management]
    end

    subgraph AdvisoryEngine["Advisory Engine"]
        CreateAdv[Create / Bulk Advisories]
        ViewAdv[View & Filter Advisories]
        Weather[Weather Logging]
        Save[Save Guides]
    end

    Auth --> Users
    Users --> CreateAdv
    Crops --> CreateAdv
    Soil --> CreateAdv
    Seasons --> CreateAdv
    CreateAdv --> ViewAdv
    ViewAdv --> Weather
    ViewAdv --> Save
```

---

## 8. Future Enhancements

```mermaid
mindmap
  root((CAS))
    Current
      Farmer Auth
      Crop / Soil / Season
      Advisories
      Weather Logs
      Cloudinary Images
    Next
      Real-time Weather API
      SMS / WhatsApp Alerts
      Farmer Dashboard
      Analytics
    Later
      Mobile App
      Multi-language
      AI Crop Recommendations
      Marketplace Integration
```

---

**Notes**

- Diagrams are written in Mermaid and render on GitHub, GitLab, Notion, and VS Code.
- Update this file as new features are added.
