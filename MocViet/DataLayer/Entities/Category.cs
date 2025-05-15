using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Entities;

public partial class Category : BaseEntity
{
    // Removed CategoryId property (use BaseEntity.Id)

    public string Name { get; set; } = null!;
    public string? Description { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
