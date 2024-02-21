# TicketHandler

### Roadmap

- [x] Crud Holders, Sectors, Users and Tickets
- [x] Create Entity Framework Mapping and Context
- [x] Apply InMemoryDatabase for testing
- [x] Create basic authentication
- [ ] Implement JWT

- [ ] Get tickets by holder
- [ ] Get tickets by assignee
- [ ] Get tickets by sector
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
