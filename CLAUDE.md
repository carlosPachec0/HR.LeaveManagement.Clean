# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project overview

HR.LeaveManagement.Clean is a learning project based on the Udemy course "ASP.NET Core Solid & Clean Architecture" — a Clean Architecture / CQRS implementation of an HR Leave Management system in .NET 9.

The `.sln` is organized with solution folders (`src`, `Core`, `Infrastructure`, `UI`, `API`, `test`) as placeholders for the typical Clean Architecture layout, but only two projects exist on disk today: `HR.LeaveManagement.Domain` and `HR.LeaveManagement.Application`. The other projects will be added later as the course progresses.

## Build & run

Target framework: **net9.0**. .NET 9 SDK is required (verified present: 9.0.102, 9.0.315).

```bash
# Restore + build the whole solution
dotnet restore HR.LeaveManagement.Clean.sln
dotnet build HR.LeaveManagement.Clean.sln

# Build a single project
dotnet build HR.LeaveManagement.Application/HR.LeaveManagement.Application.csproj

# Run a single test project (once tests exist)
dotnet test HR.LeaveManagement.Tests/HR.LeaveManagement.Tests.csproj
```

There are no test projects yet. There is no `dotnet new` template — the solution file already exists; new projects should be added with `dotnet sln add` and placed into the appropriate solution folder.

## Solution layout (intended Clean Architecture)

```
HR.LeaveManagement.Clean.sln
├── HR.LeaveManagement.Domain          ← entities, value objects, domain events (no deps)
│   └── Common/BaseEntity.cs           ← shared base (Id, DateCreated, DateModified)
│   ├── LeaveType.cs
│   ├── LeaveAllocation.cs
│   └── LeaveRequest.cs
└── HR.LeaveManagement.Application     ← use cases, contracts, DTOs, validation, mapping
    ├── Contracts/Persistence/         ← repository interfaces (no impls here)
    │   ├── IGenericRepository<T>
    │   ├── ILeaveTypeRepository
    │   ├── ILeaveAllocationRepository
    │   └── ILeaveRequestRepository
    └── ApplicationServiceRegistration.cs ← DI extension method (AddApplicationServices)
```

Planned but not yet present on disk: `HR.LeaveManagement.Infrastructure` (EF Core, repository impls), `HR.LeaveManagement.API` (controllers / minimal API host), and test projects under `test/`.

## Key architectural rules

- **Dependency direction is inward only**: `Application` references `Domain`; future `Infrastructure` and `API` reference `Application`. `Domain` has zero project references.
- **Domain entities** inherit from `BaseEntity` (Id, DateCreated, DateModified). Nullable reference types are enabled — domain strings default to `string.Empty`, navigation properties use `?`.
- **Persistence contracts live in Application**, not Domain. Concrete repositories belong in `Infrastructure` once that project is added.
- **`IGenericRepository<T>` is incomplete** — current signature has no `Task<T> GetAsync()` overload parameter (no predicate, no includes, no paging). Expect to extend it when EF Core is wired up.
- **DI registration pattern**: each layer exposes a static extension method (`AddApplicationServices`, future `AddInfrastructureServices`) called from the API composition root.

## NuGet packages in use

`HR.LeaveManagement.Application` currently references:

- `AutoMapper` 16.1.1 — **note: this package alone does not provide `AddAutoMapper`**. For the DI extension used in Clean Architecture courses, add `AutoMapper.Extensions.Microsoft.DependencyInjection` (and pin `AutoMapper` to a 13.x version compatible with that extension). This is the cause of the CS1503 error: `AddAutoMapper` from the wrong package resolves to a non-existent overload.
- `MediatR` 14.1.0 — for CQRS handlers / notifications.

`HR.LeaveManagement.Domain` has no NuGet dependencies.

## Conventions observed in the existing code

- File-scoped namespaces (`namespace HR.LeaveManagement.Domain;`).
- `<ImplicitUsings>enable</ImplicitUsings>` and `<Nullable>enable</Nullable>` in both `.csproj` files.
- XML doc comments on public domain properties (some have copy/paste typos — e.g. `LeaveType.DefaultDays` is documented as "Name property"; worth fixing when touching that file).
- `[ForeignKey]` attributes used directly on domain entities (acceptable for a learning project; in production this often moves to the EF Core configuration in `Infrastructure`).
