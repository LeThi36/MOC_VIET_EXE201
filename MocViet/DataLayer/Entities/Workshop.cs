using System;
using System.Collections.Generic;

namespace DataLayer.Entities;

public partial class Workshop : BaseEntity
{
    // Removed WorkshopId property (use BaseEntity.Id)

    public string HostId { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? LocationId { get; set; }
    public DateTime? StartTime { get; set; }
    public DateTime? EndTime { get; set; }
    public int? MaxParticipants { get; set; }
    public decimal? Fee { get; set; }
    public bool? IsActive { get; set; }

    public virtual User Host { get; set; } = null!;
    public virtual Location? Location { get; set; }
    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
    public virtual ICollection<WorkshopRegistration> WorkshopRegistrations { get; set; } = new List<WorkshopRegistration>();
}
