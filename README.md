# Ecommerce Platform

## Overview

Ecommerce platform following DDD practices, Clean Architecture and CQRS

## Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet) installed.
- Database: Ensure you have [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) running locally.

### Installation

1. Clone the repository

```bash
git clone https://github.com/dagweg/Ecommerce_Platform.NET.git
```

2. Install dependencies

```bash
dotnet restore .\Ecommerce.Presentation\Api\Ecommerce.Api.csproj
dotnet tool restore
```

3. Run the Tests

```bash
dotnet test .\Ecommerce.Tests\Ecommerce.Tests.csproj
```

4. Run the Api

```bash
dotnet run --project .\Ecommerce.Presentation\Api\Ecommerce.Api.csproj
```
