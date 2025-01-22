# Ecommerce Platform

<div align="center">
<img src="https://img.shields.io/badge/ASP.NET-blue?style=for-the-badge&logo=dotnet&logoColor=white" alt=".NET">
<img src="https://img.shields.io/badge/Automapper-orange?style=for-the-badge&logo=automapper&logoColor=white" alt="Automapper">
<img src="https://img.shields.io/badge/Fluent%20Validation-purple?style=for-the-badge&logo=fluent&logoColor=white" alt="Fluent Validation">
<img src="https://img.shields.io/badge/xUnit-lightblue?style=for-the-badge&logo=xunit&logoColor=white" alt="xUnit">
<img src="https://img.shields.io/badge/Serilog-pink?style=for-the-badge&logo=serilog&logoColor=white" alt="Serilog">
<img src="https://img.shields.io/badge/Moq-green?style=for-the-badge&logo=moq&logoColor=white" alt="Moq">
<img src="https://img.shields.io/badge/MediatR-red?style=for-the-badge&logo=Mediatr&logoColor=white" alt="Mediatr">
<img src="https://img.shields.io/badge/OpenAPI%20Swagger-green?style=for-the-badge&logo=swagger&logoColor=white" alt="Swagger">
<img src="https://img.shields.io/badge/Entity%20Framework%20Core-darkblue?style=for-the-badge&logo=entity-framework-core&logoColor=white" alt="EfCore">
<img src="https://img.shields.io/badge/Microsoft%20SQL%20Server-brown?style=for-the-badge&logo=microsoft-sql-server&logoColor=white" alt="MSSQL">
</div>

## Overview

An Ecommerce WebService built with .NET Core following Clean Architecture, DDD, CQRS and SOLID principles.
![image](https://github.com/user-attachments/assets/4df82ff1-acc2-4c54-9da4-eef30079d942)


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
