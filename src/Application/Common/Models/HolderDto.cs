﻿using Domain.Entities;

namespace Application.Common.Models;

public class HolderDto
{
    public required string Name { get; set; }
    public List<SectorEntity>? Sectors { get; set; }
}
