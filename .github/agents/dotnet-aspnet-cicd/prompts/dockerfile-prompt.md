# Containerization Guidance for ASP.NET Core

Prompt:
Produce containerization guidance and artifacts for ASP.NET Core applications.

When executing (Execution mode) consider:
- Multi-stage build (build + runtime) using official Microsoft images.
- Non-root runtime user and minimized runtime surface.
- Use appropriate SDK/runtime versions and produce .dockerignore recommendations.

Note: This file is a prompt template only. Do not generate Dockerfiles while in Agent Definition mode.