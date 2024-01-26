# TicketHandler

### Roadmap

- [ ] Create a ticket
- [ ] Delete a ticket
- [ ] Update a ticket
- [ ] Get all tickets
- [ ] Get a ticket by id
- [ ] Get all tickets by user id
- [ ] Get all tickets by user id and status

- [ ] Create a user
- [ ] Delete a user
- [ ] Update a user
- [ ] Get all users
- [ ] Get a user by id

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
