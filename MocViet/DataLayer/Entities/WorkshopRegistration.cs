using System;
using System.Collections.Generic;

namespace DataLayer.Entities;

public partial class WorkshopRegistration : BaseEntity
{
    // Removed RegistrationId property (use BaseEntity.Id)

    public string WorkshopId { get; set; }
    public string UserId { get; set; }
    public DateTime? RegisteredAt { get; set; }
    public string? Status { get; set; }

    public virtual User User { get; set; } = null!;
    public virtual Workshop Workshop { get; set; } = null!;
}
