# RecipesAPI

REST API to create/update/delete recipes

Requirements:
* dotnet core 5.0.102
* postgres

Packages:
* xunit

## Migrations
For database migrations: https://docs.microsoft.com/en-us/ef/core/managing-schemas/migrations/?tabs=dotnet-core-cli


### Dotnet commands
```c#
dotnet restore
dotnet build --no-restore
dotnet test

// run specific test
dotnet dotnet test --filter "FullyQualifiedName=YourNamespace.TestClass1.Test1"

```