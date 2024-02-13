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
            var context = serviceProvider.GetRequiredService<DataContext>();

            if (context.Database.ProviderName != "Microsoft.EntityFrameworkCore.InMemory")
            {
                context.Database.Migrate();
            }

            var storedHolders = context.Holders.ToList();
            var storedSectors = context.Sectors.ToList();
            var storedUsers = context.Users.ToList();

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

            if (!context.Tickets.Any())
            {
                var tickets = new List<TicketEntity>
                {
                    new() {
                        Title = "Ticket 1",
                        Content = "Content 1",
                        Status = TicketStatus.Open,
                        Priority = TicketPriority.Low,
                        AssigneeId = context.Users.FirstOrDefault()!.Id,
                        HolderId = context.Holders.FirstOrDefault()!.Id,
                        SectorId = context.Sectors.FirstOrDefault()!.Id,
                        UserId = context.Users.FirstOrDefault()!.Id,
                    },
                    new() {
                        Title = "Ticket 2",
                        Content = "Content 2",
                        Status = TicketStatus.Open,
                        Priority = TicketPriority.Low,
                        AssigneeId = context.Users.FirstOrDefault()!.Id,
                        HolderId = context.Holders.FirstOrDefault()!.Id,
                        SectorId = context.Sectors.FirstOrDefault()!.Id,
                        UserId = context.Users.FirstOrDefault()!.Id
                    }
                };

                await context.Tickets.AddRangeAsync(tickets);
                await context.SaveChangesAsync();
            }
        }
    }
}
