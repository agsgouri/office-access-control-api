🏢 Office Access Control System API

Enterprise-grade Access Control and Attendance Management backend built with ASP.NET Core, Clean Architecture, and JWT-based authentication.

This project simulates a real-world office security system that manages:

Employee access validation

Role-based authorization

Attendance tracking (IN / OUT sessions)

Supervisor hierarchy

Flagged access monitoring

🚀 Tech Stack

ASP.NET Core Web API

Clean Architecture (Core / Application / Infrastructure / API)

Entity Framework Core

SQL Server

ASP.NET Identity

JWT Authentication

Policy-Based Authorization

IMemoryCache

Hosted Background Service (architecture prepared)

📦 Solution Structure
OfficeAccessControl.API.sln
│
├── OfficeAccessControl.API          → Presentation Layer
├── OfficeAccessControl.Application  → Business Logic Layer
├── OfficeAccessControl.Core         → Domain Models & Interfaces
└── OfficeAccessControl.Infrastructure → EF Core, Identity, DB Access

**Layer Responsibilities:-**
**Core**

Domain models
Repository contracts
Business enums

**Application**

Service contracts
Business services
DTOs
Authorization policies

**Infrastructure**

EF Core DbContext
Identity implementation
Repository implementations
Background services (architecture ready)

**API**

Controllers
JWT configuration
Middleware
Filters

🔐 Authentication & Authorization
JWT Authentication
User registers
User logs in
JWT token issued
Token includes: UserId Roles Claims
Authentication handled by ASP.NET middleware.
Role-Based Authorization
Policy-Based Authorization

**Custom policies implemented for:**

Supervisor can view subordinates
Employee can only view own attendance

Role-based endpoint protection

🧾 Core Implemented Features
Access Validation
Validates:
User existence
User active status
Location existence
Role allowed for location
Generates AccessLog entry
Flags abnormal attempts

2️⃣ Attendance Management

Supports:
Multiple IN / OUT sessions per day
Total minutes calculation
Session tracking
Open session detection
OUT without IN handling
Cross-day logic support (night shift ready design)

3️⃣ Supervisor Hierarchy

Users can have SupervisorId
Hierarchical access control
Policy-based restriction
Supervisor can view employee data

4️⃣ Caching

Attendance summary caching via IMemoryCache
Reduces repeated DB reads

5️⃣ Background Processing Architecture (Designed)

Flagged access logs persisted
Designed for background notification processing
Architecture ready for:
Email alerts
Async processing
Outbox-like pattern
(Currently conceptual – not fully enabled)


🧪 Testing Flow (Postman)
1️⃣ Register

POST /api/auth/register

2️⃣ Login

POST /api/auth/login
→ Receive JWT

3️⃣ Validate Access

POST /api/access?userId=xxx&locationId=xxx&direction=In

4️⃣ Get Attendance

GET /api/attendance/{userId}
(Authorization required)

🛠 How To Run

Clone repository

Update connection string in appsettings.json

Run migrations:

dotnet ef database update

Run API:

dotnet run

Access Swagger UI

📈 Near Future Enhancements

Planned improvements:

🔹 Background Alert Processing

Hosted service to process flagged logs

Supervisor email notification

Non-blocking architecture

🔹 Weekly Attendance Analytics

Average IN time

Average OUT time

Average working hours

Supervisor reporting dashboard API

🔹 Advanced Anomaly Detection

Unusual time detection

Location misuse detection

Behavior-based flagging

🔹 Soft Delete & Auditing

CreatedAt

UpdatedAt

DeletedAt

Global query filters

🔹 Pagination & Filtering

Attendance history

Access logs

🔹 Structured Logging & Correlation IDs

Request tracing

Improved observability

🎯 Architectural Highlights

Clean separation of concerns
No domain logic inside controllers
Application layer independent of infrastructure
Identity integrated without violating architecture
Policy-based authorization
Async-ready system design

🧠 Design Philosophy

The system is built not as a CRUD API, but as a scalable backend foundation that supports:

Security-first design
Hierarchical authorization
Extensible attendance modeling
Async alert processing
Real-world enterprise patterns
