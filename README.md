# TicketHandler

### Roadmap

- [x] Create basic crud operations
- [x] Create basic authentication
- [ ] Implement JWT

- [ ] Get tickets by holder
- [ ] Get tickets by assignee
- [ ] Get tickets by sector
- [ ] Get tickets by holder
- [ ] Get tickets by status

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
