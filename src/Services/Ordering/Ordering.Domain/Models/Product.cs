﻿using Ordering.Domain.Abstractions;

namespace Ordering.Domain.Models;

public class Product : Entity<Guid>
{
    public string Name { get; private set; }
    public string Price { get; private set; }

}
