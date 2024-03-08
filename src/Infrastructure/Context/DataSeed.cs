using Domain.Constants;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Context
{
    public class DataSeed
    {
        public static async Task SeedAsync(IServiceProvider serviceProvider)
        {
            try
            {

                var context = serviceProvider.GetRequiredService<DataContext>();

                if (context.Database.ProviderName != "Microsoft.EntityFrameworkCore.InMemory")
                {
                    context.Database.Migrate();
                }

                var storedHolders = context.Holders.ToList();
                var storedSectors = context.Sectors.ToList();
                var storedUsers = context.Users.ToList();
                var storedTickets = context.Tickets.ToList();
                var storedStatuses = context.Statuses.ToList();
                var storedPriorities = context.Priorities.ToList();

                if (!context.Statuses.Any())
                {
                    var statuses = new List<StatusEntity>
                {
                    new() {
                        Code = "Open",
                        Description = "Open"
                    },
                    new() {
                        Code = "Active",
                        Description = "Active"
                    },
                    new() {
                        Code = "Closed",
                        Description = "Closed"
                    }
                };

                    await context.Statuses.AddRangeAsync(statuses);
                    await context.SaveChangesAsync();
                }

                if (!context.Priorities.Any())
                {
                    var priorities = new List<PriorityEntity>
                {
                    new() {
                        Code = "Low",
                        Description = "Low"
                    },
                    new() {
                        Code = "Medium",
                        Description = "Medium"
                    },
                    new() {
                        Code = "High",
                        Description = "High"
                    }
                };

                    await context.Priorities.AddRangeAsync(priorities);
                    await context.SaveChangesAsync();
                }

                if (!storedHolders.Where(x => x.Name == "Holder").Any())
                {
                    var holders = new List<HolderEntity>
                {
                    new() {
                        Name = "Holder",
                    }
                };

                    await context.Holders.AddRangeAsync(holders);
                    await context.SaveChangesAsync();
                }

                if (!storedSectors.Where(x => x.Name == "Office 1").Any() || !storedSectors.Where(x => x.Name == "Office 2").Any())
                {
                    var sectors = new List<SectorEntity>
                {
                    new() {
                        Name = "Office 1",
                        HolderId = context.Holders.FirstOrDefault()!.Id
                    },
                    new() {
                        Name = "Office 2",
                        HolderId = context.Holders.FirstOrDefault()!.Id
                    }
                };

                    await context.Sectors.AddRangeAsync(sectors);
                    await context.SaveChangesAsync();
                }

                if (!storedUsers.Where(x => x.Name == "Admin").Any())
                {
                    var users = new List<UserEntity>
                {
                    new() {
                        Name = "Admin",
                        Email = "admin@tickethandler.com",
                        Password = "admin",
                        Role = UserRoles.Administrator,
                        HolderId = context.Holders.FirstOrDefault()!.Id,
                        Sectors = [.. context.Sectors]
                    }
                };

                    await context.Users.AddRangeAsync(users);
                    await context.SaveChangesAsync();
                }

                var statusOpen = context.Statuses.FirstOrDefault(x => x.Code == "Open");
                var priorityLow = context.Priorities.FirstOrDefault(x => x.Code == "Low");

                if (!context.Tickets.Any())
                {
                    var tickets = new List<TicketEntity>
                {
                    new() {
                        Title = "Ticket 1",
                        Content = "Content 1",
                        StatusId = statusOpen!.Id,
                        PriorityId = priorityLow!.Id,
                        AssigneeId = context.Users.FirstOrDefault()!.Id,
                        HolderId = context.Holders.FirstOrDefault()!.Id,
                        SectorId = context.Sectors.FirstOrDefault()!.Id,
                        UserId = context.Users.FirstOrDefault()!.Id,
                    },
                    new() {
                        Title = "Ticket 2",
                        Content = "Content 2",
                        StatusId = statusOpen!.Id,
                        PriorityId = priorityLow!.Id,
                        AssigneeId = context.Users.FirstOrDefault()!.Id,
                        HolderId = context.Holders.FirstOrDefault()!.Id,
                        SectorId = context.Sectors.FirstOrDefault()!.Id,
                        UserId = context.Users.FirstOrDefault()!.Id
                    },
                    new() {
                        Title = "Ticket 3",
                        Content = "Content 3",
                        StatusId = statusOpen!.Id,
                        PriorityId = priorityLow!.Id,
                        AssigneeId = context.Users.FirstOrDefault()!.Id,
                        HolderId = context.Holders.FirstOrDefault()!.Id,
                        SectorId = context.Sectors.FirstOrDefault()!.Id,
                        UserId = context.Users.FirstOrDefault()!.Id
                    },
                    new() {
                        Title = "Ticket 4",
                        Content = "Content 4",
                        StatusId = statusOpen!.Id,
                        PriorityId = priorityLow!.Id,
                        AssigneeId = context.Users.FirstOrDefault()!.Id,
                        HolderId = context.Holders.FirstOrDefault()!.Id,
                        SectorId = context.Sectors.FirstOrDefault()!.Id,
                        UserId = context.Users.FirstOrDefault()!.Id
                    },
                    new() {
                        Title = "Ticket 5",
                        Content = "Content 5",
                        StatusId = statusOpen!.Id,
                        PriorityId = priorityLow!.Id,
                        AssigneeId = context.Users.FirstOrDefault()!.Id,
                        HolderId = context.Holders.FirstOrDefault()!.Id,
                        SectorId = context.Sectors.FirstOrDefault()!.Id,
                        UserId = context.Users.FirstOrDefault()!.Id
                    },
                    new() {
                        Title = "Ticket 6",
                        Content = "Content 6",
                        StatusId = statusOpen!.Id,
                        PriorityId = priorityLow!.Id,
                        AssigneeId = context.Users.FirstOrDefault()!.Id,
                        HolderId = context.Holders.FirstOrDefault()!.Id,
                        SectorId = context.Sectors.FirstOrDefault()!.Id,
                        UserId = context.Users.FirstOrDefault()!.Id
                    },
                    new() {
                        Title = "Ticket 7",
                        Content = "Content 7",
                        StatusId = statusOpen!.Id,
                        PriorityId = priorityLow!.Id,
                        AssigneeId = context.Users.FirstOrDefault()!.Id,
                        HolderId = context.Holders.FirstOrDefault()!.Id,
                        SectorId = context.Sectors.FirstOrDefault()!.Id,
                        UserId = context.Users.FirstOrDefault()!.Id
                    },
                    new() {
                        Title = "Ticket 8",
                        Content = "Content 8",
                        StatusId = statusOpen!.Id,
                        PriorityId = priorityLow!.Id,
                        AssigneeId = context.Users.FirstOrDefault()!.Id,
                        HolderId = context.Holders.FirstOrDefault()!.Id,
                        SectorId = context.Sectors.FirstOrDefault()!.Id,
                        UserId = context.Users.FirstOrDefault()!.Id
                    },
                    new() {
                        Title = "Ticket 9",
                        Content = "Content 9",
                        StatusId = statusOpen!.Id,
                        PriorityId = priorityLow!.Id,
                        AssigneeId = context.Users.FirstOrDefault()!.Id,
                        HolderId = context.Holders.FirstOrDefault()!.Id,
                        SectorId = context.Sectors.FirstOrDefault()!.Id,
                        UserId = context.Users.FirstOrDefault()!.Id
                    },
                    new() {
                        Title = "Ticket 10",
                        Content = "Content 10",
                        StatusId = statusOpen!.Id,
                        PriorityId = priorityLow!.Id,
                        AssigneeId = context.Users.FirstOrDefault()!.Id,
                        HolderId = context.Holders.FirstOrDefault()!.Id,
                        SectorId = context.Sectors.FirstOrDefault()!.Id,
                        UserId = context.Users.FirstOrDefault()!.Id
                    },
                    new() {
                        Title = "Ticket 11",
                        Content = "Content 11",
                        StatusId = statusOpen!.Id,
                        PriorityId = priorityLow!.Id,
                        AssigneeId = context.Users.FirstOrDefault()!.Id,
                        HolderId = context.Holders.FirstOrDefault()!.Id,
                        SectorId = context.Sectors.FirstOrDefault()!.Id,
                        UserId = context.Users.FirstOrDefault()!.Id
                    },
                    new() {
                        Title = "Ticket 12",
                        Content = "Content 12",
                        StatusId = statusOpen!.Id,
                        PriorityId = priorityLow!.Id,
                        AssigneeId = context.Users.FirstOrDefault()!.Id,
                        HolderId = context.Holders.FirstOrDefault()!.Id,
                        SectorId = context.Sectors.FirstOrDefault()!.Id,
                        UserId = context.Users.FirstOrDefault()!.Id
                    },
                    new() {
                        Title = "Ticket 13",
                        Content = "Content 13",
                        StatusId = statusOpen!.Id,
                        PriorityId = priorityLow!.Id,
                        AssigneeId = context.Users.FirstOrDefault()!.Id,
                        HolderId = context.Holders.FirstOrDefault()!.Id,
                        SectorId = context.Sectors.FirstOrDefault()!.Id,
                        UserId = context.Users.FirstOrDefault()!.Id
                    },
                    new() {
                        Title = "Ticket 14",
                        Content = "Content 14",
                        StatusId = statusOpen!.Id,
                        PriorityId = priorityLow!.Id,
                        AssigneeId = context.Users.FirstOrDefault()!.Id,
                        HolderId = context.Holders.FirstOrDefault()!.Id,
                        SectorId = context.Sectors.FirstOrDefault()!.Id,
                        UserId = context.Users.FirstOrDefault()!.Id
                    },
                    new() {
                        Title = "Ticket 15",
                        Content = "Content 15",
                        StatusId = statusOpen!.Id,
                        PriorityId = priorityLow!.Id,
                        AssigneeId = context.Users.FirstOrDefault()!.Id,
                        HolderId = context.Holders.FirstOrDefault()!.Id,
                        SectorId = context.Sectors.FirstOrDefault()!.Id,
                        UserId = context.Users.FirstOrDefault()!.Id
                    },
                    new() {
                        Title = "Ticket 16",
                        Content = "Content 16",
                        StatusId = statusOpen!.Id,
                        PriorityId = priorityLow!.Id,
                        AssigneeId = context.Users.FirstOrDefault()!.Id,
                        HolderId = context.Holders.FirstOrDefault()!.Id,
                        SectorId = context.Sectors.FirstOrDefault()!.Id,
                        UserId = context.Users.FirstOrDefault()!.Id
                    },
                };

                    await context.Tickets.AddRangeAsync(tickets);
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
