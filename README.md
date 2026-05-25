# Devnet.Vault

Devnet.Vault is a secure vault application that includes a .NET backend API and a mobile client under the `Mobile/` directory.

## Overview

This repository contains the backend and supporting services for Devnet.Vault. The API handles authentication, vault item management, notifications, and other core application workflows.

## Features

- User authentication and authorization
- Vault item storage and management
- Notification support
- Backend infrastructure built with ASP.NET Core

## Repository structure

- `Backend/` — .NET backend solution and API projects
- `Mobile/` — mobile client source
- `.github/` — contribution and issue templates

## Prerequisites

- .NET 10 SDK
- MySQL server
- Redis server
- SMTP credentials for email delivery

## Getting started

### 1. Clone the repository

```bash
git clone https://github.com/<your-username>/Devnet.Vault.git
cd Devnet.Vault
```

### 2. Configure development settings

Update `Backend/src/Devnet.Vault.Api/appsettings.Development.json` with your local values for:

- `ConnectionStrings`
- `JwtSettings`
- `EmailSettings`
- `EncryptionSettings`

### 3. Build the solution

```bash
dotnet build Backend/Devnet.Vault.slnx
```

### 4. Run the API

```bash
dotnet run --project Backend/src/Devnet.Vault.Api/Devnet.Vault.Api.csproj
```

## Development notes

- The API uses `appsettings.Development.json` for local development settings.
- Replace the placeholder values before running the application.
- Keep secrets out of the repository.

## Testing

Run the solution build to validate the current state of the repo:

```bash
dotnet build Backend/Devnet.Vault.slnx
```

## Contributing

Please read [CONTRIBUTING.md](CONTRIBUTING.md) before opening a pull request.

## Code of conduct

This project follows the [Code of Conduct](CODE_OF_CONDUCT.md).

## Security

If you discover a security vulnerability, please review [SECURITY.md](SECURITY.md) before reporting it.

## License

This project is licensed under the MIT License. See [LICENSE](LICENSE) for details.
