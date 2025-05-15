using System;
using System.Collections.Generic;

namespace DataLayer.Entities;

public partial class CartItem : BaseEntity
{
    // Removed CartItemId property (use BaseEntity.Id)

    public string CartId { get; set; }
    public string ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }

    public virtual Cart Cart { get; set; } = null!;
    public virtual Product Product { get; set; } = null!;
}
