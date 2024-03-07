﻿using Domain.Common;

namespace Domain.Entities;

public class StatusEntity : BaseEntity
{
    public required string Code { get; set; }
    public required string Description { get; set; }

    public virtual List<TicketEntity>? Tickets { get; set; }
}
