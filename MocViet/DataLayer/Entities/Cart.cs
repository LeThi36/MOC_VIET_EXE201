using System;
using System.Collections.Generic;

namespace DataLayer.Entities;

public partial class Cart : BaseEntity
{
    public int CartId { get; set; }

    public int UserId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public bool? IsCheckedOut { get; set; }

    public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();

    public virtual User User { get; set; } = null!;
}
