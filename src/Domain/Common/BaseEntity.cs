﻿using System.Text.Json.Serialization;

namespace Domain.Common;

public abstract class BaseEntity
{
    [JsonIgnore]
    public Guid Id { get; set; }
}
