# TaptapBankClient Client

This API client, built using .NET 6, facilitates user management, financial transactions, and user status upgrades. It's designed for developers and testers who require a robust and feature-rich platform for simulating financial operations.

## Features

- **User Creation**: Easy setup of new user accounts.
- **Fund Transfers**: Enables monetary transactions to recipients.
- **User Upgrades**: Automates user status upgrades upon reaching transaction thresholds.

## Technologies Used

- `.NET 6`: A modern, high-performance, cross-platform framework.
- `Polly`: Implements resilience and transient-fault-handling.
- `Bogus`: Generates fake data for testing and development purposes.
- `Fluent Validation`: Ensures robust input validation.
- `XUnit`: Framework for unit tests.
- `libphonenumber-csharp`: Validates phone numbers efficiently.
- `Moq`: Mocks services for testing.
- `Newtonsoft Json`: Handles JSON serialization and deserialization.

## Getting Started

### Prerequisites

- .NET 6 SDK
- A suitable development environment like Visual Studio or VS Code.

### Installation

1. Clone the repository:
   ```bash
   git clone <repository URL>
   ```
2. Navigate to the project directory:
   ```bash
   cd <project directory>
   ```
3. Restore the project dependencies:
   ```bash
   dotnet restore
   ```

### Running the Application

To start the application, run:

```bash
dotnet run
```

### Running the Tests

Execute the unit tests with:

```bash
dotnet test
```