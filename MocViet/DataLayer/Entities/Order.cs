using System;
using System.Collections.Generic;

namespace DataLayer.Entities;

public partial class Order : BaseEntity
{
    public int OrderId { get; set; }

    public int BuyerId { get; set; }

    public DateTime? OrderDate { get; set; }

    public decimal? TotalAmount { get; set; }

    public string? Status { get; set; }

    public string PaymentType { get; set; } = null!;

    public virtual User Buyer { get; set; } = null!;

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
}
