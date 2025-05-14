using System;
using System.Collections.Generic;

namespace DataLayer.Entities;

public partial class Review : BaseEntity
{
    public int ReviewId { get; set; }

    public int UserId { get; set; }

    public int? ProductId { get; set; }

    public int? WorkshopId { get; set; }

    public int? Rating { get; set; }

    public string? Comment { get; set; }


    public virtual Product? Product { get; set; }

    public virtual User User { get; set; } = null!;

    public virtual Workshop? Workshop { get; set; }
}
