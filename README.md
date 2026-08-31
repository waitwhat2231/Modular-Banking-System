# Modular Banking System

A modular-monolith banking backend built in ASP.NET Core, applying Clean
Architecture and CQRS (via MediatR) across independently bounded modules —
Accounts, Transactions, Payroll, and Users.

Built as a reusable architectural template: `Template.*` is the base scaffold
(API → Application → Domain → Infrastructure) that each new module extends,
so growing the system means copying a consistent layering rather than
reinventing it per module. The same modular-monolith discipline is applied
across my other backend work — this project demonstrates the pattern as a
deliberate practice, not a one-off.

## Architecture

```
┌───────────────────────────────────────────┐
│                 API Layer                   │
│   Modules.Accounts.Endpoints, etc.           │
├───────────────────────────────────────────┤
│              Application Layer               │
│   MediatR Commands/Queries per module          │
├───────────────────────────────────────────┤
│                Domain Layer                    │
│   Entities and business rules, per module        │
├───────────────────────────────────────────┤
│             Infrastructure Layer                │
│   EF Core persistence, per module                 │
└───────────────────────────────────────────┘
```

## Modules

- **Accounts** — account creation, balance tracking, account lifecycle
  <!-- TODO: adjust to what this module actually covers -->
- **Transactions** — money movement / transfers between accounts
  <!-- TODO: adjust -->
- **Payroll** — payroll runs and disbursement logic
  <!-- TODO: adjust -->
- **Users** — user identity and access within the system
  <!-- TODO: adjust -->
- **Common.SharedClasses** — cross-module contracts and shared kernel
- **Template.\*** — the reusable scaffold new modules are built from

## Why modular monolith + CQRS

- Modular monolith: clear boundaries between modules without the
  operational overhead of running separate services
- CQRS + MediatR: separates read and write paths per module, keeping
  handlers small, testable, and independent of each other

## Tech stack

ASP.NET Core · Entity Framework Core · MediatR · xUnit · Swagger/OpenAPI

## Testing

Accounts/Transactions logic is covered by xUnit test suites in `Common.Test`:

```
dotnet test
```

## Running locally

```
dotnet restore
dotnet build
dotnet run --project Template.API
```

<!-- TODO: swap Template.API for the actual startup project if different -->

Swagger UI available at `/swagger` once running.

## Status

Personal project, actively used as a base template for new modules.
