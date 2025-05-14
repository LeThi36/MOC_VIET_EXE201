﻿using System;
using System.Collections.Generic;

namespace DataLayer.Entities;

public partial class Category : BaseEntity
{
    public int CategoryId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
