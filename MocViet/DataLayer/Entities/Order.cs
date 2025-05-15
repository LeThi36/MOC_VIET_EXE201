using System;
using System.Collections.Generic;

namespace DataLayer.Entities;

public partial class Order : BaseEntity
{
    // Removed OrderId property (use BaseEntity.Id)

    public string BuyerId { get; set; }
    public DateTime? OrderDate { get; set; }
    public decimal? TotalAmount { get; set; }
    public string? Status { get; set; }
    public string PaymentType { get; set; } = null!;

    public virtual User Buyer { get; set; } = null!;
    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
}
