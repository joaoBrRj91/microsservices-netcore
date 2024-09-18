using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Domain.Abstractions;

public abstract class Entity<TIdentity> : IEntity<TIdentity>
{
    public abstract TIdentity Id { get; set; }
    public abstract DateTime? CreateAt { get; set; }
    public abstract string? CreateBy { get; set; }
    public abstract DateTime? LastModified { get; set; }
    public abstract string? LastModifiedBy { get; set; }
}
