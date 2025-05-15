using System;
using System.Collections.Generic;

namespace DataLayer.Entities;

public partial class OrderDetail : BaseEntity
{
    // Removed OrderDetailId property (use BaseEntity.Id)

    public string OrderId { get; set; }
    public string ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }

    public virtual Order Order { get; set; } = null!;
    public virtual Product Product { get; set; } = null!;
}
