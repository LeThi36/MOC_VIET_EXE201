using System;
using System.Collections.Generic;

namespace DataLayer.Entities;

public partial class Review : BaseEntity
{
    // Removed ReviewId property (use BaseEntity.Id)

    public string UserId { get; set; }
    public string? ProductId { get; set; }
    public string? WorkshopId { get; set; }
    public int? Rating { get; set; }
    public string? Comment { get; set; }

    public virtual Product? Product { get; set; }
    public virtual User User { get; set; } = null!;
    public virtual Workshop? Workshop { get; set; }
}
