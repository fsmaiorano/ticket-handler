# TicketHandler

### Description

This project is a ticket handler system, where you can create tickets, assign them to a user, and add comments to the ticket.

### How to run

Before running the project, you need to create a database and run the migrations.

```bash
docker-compose up
```

After that, you can run the project.

```bash
dotnet run
```

### How to test

```bash
dotnet test
```

### Technologies

- .NET Core 8
- Entity Framework Core
- SQL Server
- Redis
- Docker
- Docker Compose
- MsTest
- Moq
- FluentAssertions
- Swagger
- MediatR
- AutoMapper
- Serilog

### Roadmap

## Backend

## Frontend

- [ ] Implement skeleton loading

### To Refactor

- [ ] Ticket Status and Priority should be an enum and stored in the database

### Commands

### Create migration

```bash
dotnet ef migrations add MigrationName
```

### Update database

```bash
dotnet ef database update
```

#### Query to drop database

```sql
USE master;
ALTER DATABASE tickethandler SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
DROP DATABASE tickethandler ;
```
