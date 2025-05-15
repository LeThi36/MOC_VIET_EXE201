using System;
using System.Collections.Generic;

namespace DataLayer.Entities;

public partial class User : BaseEntity
{
    // Removed UserId property (use BaseEntity.Id)

    public string FullName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;
    public string? Phone { get; set; }
    public string? Address { get; set; }
    public int UserRole { get; set; }
    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
    public virtual ICollection<WorkshopRegistration> WorkshopRegistrations { get; set; } = new List<WorkshopRegistration>();
    public virtual ICollection<Workshop> Workshops { get; set; } = new List<Workshop>();
}
