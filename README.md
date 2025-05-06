# LibPort

The project is divided into several folders : Contexts, Controllers, Models, Services

- Models is the domain part, contains entity models corresponding to database tables
- Services contains business logic layer: login/refresh handling, jwt generating, book, request handlling...
- Contexts contains EF DbContext and configurations
- Controllers holds the API endpoints
- Other folder contains exception handlers(middleware), ef migrations, exceptions definition...
