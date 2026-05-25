# Contributing to Devnet.Vault

Thank you for your interest in contributing to Devnet.Vault.

## Ways to contribute

- Report bugs
- Suggest features
- Improve documentation
- Submit code changes
- Review pull requests

## Before you start

1. Fork the repository.
2. Create a feature branch from `main`.
3. Make your changes in small, focused commits.
4. Run the build locally before opening a pull request.
5. Open a pull request with a clear description of the change.

## Development setup

### Prerequisites

- .NET 10 SDK
- A MySQL instance
- A Redis instance
- A local SMTP server or SMTP credentials for testing emails

### Run locally

```bash
dotnet build Backend/Devnet.Vault.slnx

dotnet run --project Backend/src/Devnet.Vault.Api/Devnet.Vault.Api.csproj
```

### Configuration

Copy `Backend/src/Devnet.Vault.Api/appsettings.Development.json` and replace the placeholder values with your own local settings.

## Pull request guidelines

- Keep changes small and easy to review.
- Explain the problem you are solving.
- Include screenshots or logs when helpful.
- Update documentation when behavior changes.
- Ensure the solution builds cleanly.

## Code style

- Follow the existing C# conventions in the repository.
- Prefer readable, testable code.
- Avoid committing secrets or personal credentials.

## Questions

Open an issue if you are unsure about the right approach.
