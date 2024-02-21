# TicketHandler

### Description
This project is a ticket handler system, where you can create tickets, assign them to a user, and add comments to the ticket.

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

- [x] Crud Holders, Sectors, Users and Tickets
- [x] Create Entity Framework Mapping and Context
- [x] Apply InMemoryDatabase for testing
- [x] Create basic authentication
- [ ] Implement JWT

- [x] Get tickets by holder
- [x] Get tickets by assignee
- [x] Get tickets by sector
- [ ] Get tickets by user
- [ ] Get tickets by status
- [ ] Get tickets by priority

- [ ] Filter tickets by status
- [ ] Filter tickets by priority
- [ ] Filter tickets by date
- [ ] Filter tickets by status
- [ ] Filter tickets by priority
- [ ] Filter tickets by assignee
- [ ] Filter tickets by sector
- [ ] Filter tickets by user

- [ ] Assign ticket to user

- [ ] Create comment on ticket

- [ ] Create Guid to trace User session requests
- [ ] Create handler to write integration tests in database to help populate data
- [ ] Create another container with redis to store logs

### Commands

### Create migration

```bash
dotnet ef migrations add InitialCreate
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
