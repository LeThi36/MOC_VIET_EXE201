using System;
using System.Collections.Generic;

namespace DataLayer.Entities;

public partial class Location : BaseEntity
{
    public int LocationId { get; set; }

    public string? Province { get; set; }

    public string? District { get; set; }

    public string? Address { get; set; }

    public double? Latitude { get; set; }

    public double? Longitude { get; set; }

    public virtual ICollection<Workshop> Workshops { get; set; } = new List<Workshop>();
}
