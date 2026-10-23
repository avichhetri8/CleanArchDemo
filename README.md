# Clean Architecture in .NET (minimal demo)

    Api ───────────► Infrastructure ──► Application ──► Domain
     └──────────────────────────────────►┘

Dependencies point INWARD only.

| Layer          | Contains                                  | Knows about         |
|----------------|-------------------------------------------|---------------------|
| Domain         | Entities, business rules                  | nothing             |
| Application    | Use cases, interfaces (ports), DTOs       | Domain              |
| Infrastructure | Repositories, DB, external services       | Application         |
| Api            | Endpoints, DI wiring (composition root)   | Application + Infra |

Key idea: Application declares `ITodoRepository`; Infrastructure implements it.
Swap InMemory for EF Core by changing one line in Infrastructure/DependencyInjection.cs.

Run:
    dotnet new sln -n CleanArchDemo
    dotnet sln add src/Domain src/Application src/Infrastructure src/Api
    dotnet run --project src/Api

Try:
    curl -X POST http://localhost:5000/todos -H "Content-Type: application/json" -d '{"title":"Learn clean arch"}'
    curl http://localhost:5000/todos
