# Conduit

A minimal ASP.NET Core 8 Web API, structured to match the `.NET CI Pipeline`
you already have (build → test → package → docker).

## Structure

```
Conduit.sln
src/
  Conduit/
    Conduit.csproj          # matches PROJECT_FILE in the workflow
    Program.cs
    Controllers/
      ArticlesController.cs
      HealthController.cs
    Models/
      Article.cs
    Services/
      IArticleRepository.cs
    appsettings.json
tests/
  Conduit.Tests/
    Conduit.Tests.csproj    # xUnit + WebApplicationFactory
    HealthEndpointTests.cs
    ArticleRepositoryTests.cs
Dockerfile
```

## Endpoints

- `GET /health` — simple health check (also used by the Docker `HEALTHCHECK`)
- `GET /api/articles` — list articles
- `GET /api/articles/{slug}` — get one article
- `POST /api/articles` — create an article
- `/swagger` — API docs (Development environment only)

## Run locally

```bash
dotnet run --project src/Conduit/Conduit.csproj
```

## Test locally

```bash
dotnet test tests/Conduit.Tests/Conduit.Tests.csproj
```

## One thing to double check in your workflow

Your `test` job runs `dotnet test` against `${{ env.PROJECT_FILE }}`
(`src/Conduit/Conduit.csproj`), which is the **app** project, not the
**test** project — an app project has no tests, so `dotnet test` will
just report "no tests found" and pass trivially rather than actually
running `Conduit.Tests`.

To actually execute the tests in CI, either:

1. Add a second env var and point the test step at it, e.g.:
   ```yaml
   env:
     TEST_PROJECT_FILE: "./tests/Conduit.Tests/Conduit.Tests.csproj"
   ```
   and change the `test` job's restore/build/test steps to use
   `TEST_PROJECT_FILE`, or
2. Point `dotnet test` at the solution file (`Conduit.sln`) instead,
   which will build and test everything referenced by it.

Everything else in your pipeline (`restore`, `build`, `publish`, Docker
build) works as-is against `src/Conduit/Conduit.csproj`.


