# Generate CI/CD Pipeline for .NET / ASP.NET

Prompt:
Analyze the repository and generate a production-ready GitHub Actions CI/CD pipeline for a .NET / ASP.NET application.

When executing (Execution mode) consider:
- Detect .sln and .csproj files and target frameworks.
- Use dotnet restore, dotnet build, dotnet test, and dotnet publish.
- Support matrix builds for target frameworks and OSes when required.
- Publish artifacts (NuGet packages or app artifacts) when requested.

Note: This is a prompt template. In Agent Definition mode do not emit workflow files; only use this as a template until explicit Execution is requested.