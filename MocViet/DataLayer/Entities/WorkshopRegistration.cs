using System;
using System.Collections.Generic;

namespace DataLayer.Entities;

public partial class WorkshopRegistration : BaseEntity
{
    public int RegistrationId { get; set; }

    public int WorkshopId { get; set; }

    public int UserId { get; set; }

    public DateTime? RegisteredAt { get; set; }

    public string? Status { get; set; }

    public virtual User User { get; set; } = null!;

    public virtual Workshop Workshop { get; set; } = null!;
}
