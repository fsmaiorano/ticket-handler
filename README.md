# TicketHandler

### Description

This project is a ticket handler system, where you can create tickets, assign them to a user, and add comments to the ticket.
I'm working on this project to improve my skills in .NET Core and to learn new technologies.

### Features

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

- [x] Assign ticket to a user
- [ ] Add comments to a ticket
- [ ] Implement Redis cache
- [ ] Implement Dashboard - Show total tickets, tickets by status, tickets by priority
- [ ] Implement Dashboard - Show tickets assigned to the user

## Frontend

- [ ] Implement skeleton loading
- [x] Assign ticket to a user
- [ ] Add comments to a ticket


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
