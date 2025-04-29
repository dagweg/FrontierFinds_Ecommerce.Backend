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

- [.NET 9 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/9.0) installed.
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) running locally.

### Installation

1. Clone the repository

```ps1
git clone https://github.com/dagweg/Ecommerce_Platform.NET.git
```

2. Install dependencies

```ps1
dotnet restore .\Ecommerce.Presentation\Api\Ecommerce.Api.csproj
dotnet tool restore
```

3. Install EfCore globally/locally

```ps1
dotnet tool install --global dotnet-ef --version 9.0.0
```

4. Configure `appsettings.json` to suit your environment and sync migrations with db by:

```ps1
dotnet ef database update --project .\Ecommerce.Infrastructure\Ecommerce.Infrastructure.csproj

# Setup Secrets
dotnet user-secrets set "JwtSettings:SecretKey" "SECRETKEY_HERE" --project .\Ecommerce.Api
dotnet user-secrets set "EmailSettings:Password" "PASSWORD_HERE" --project .\Ecommerce.Api
```

1. Run the Tests

```ps1
dotnet test .\Ecommerce.Tests\Ecommerce.Tests.csproj
```

1. Run the Api

```ps1
dotnet run --project .\Ecommerce.Api\Ecommerce.Api.csproj
```

7. Test the endpoints! You can use RestClient and the pre-written http test requests in `.\Ecommerce.Api\HttpRequests\`.

### Additional Note:

Incase you get unexpected errors like `codeananlysis error` please use the script found in `Scripts` to remove all intermediary `obj` and `bin` files. Just like so:

```ps1
.\Scripts\cbf # Clean Build Files
```


# License

This repository is licensed under the **Creative Commons Attribution-NonCommercial 4.0 International License**.

You are free to:
- Share: Copy and redistribute the material in any medium or format.
- Adapt: Remix, transform, and build upon the material for non-commercial purposes.

Under the following terms:
- Attribution: You must give appropriate credit, provide a link to the license, and indicate if changes were made.
- Non-Commercial: You may not use the material for commercial purposes.
- No additional restrictions: You may not apply legal terms or technological measures that legally restrict others from doing anything the license permits.

Full text: https://creativecommons.org/licenses/by-nc/4.0/

