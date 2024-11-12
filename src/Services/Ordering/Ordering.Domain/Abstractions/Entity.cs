namespace Ordering.Domain.Abstractions;

public abstract class Entity<TIdentity> : IEntity
{
    //EF Core
    protected Entity() { }

    protected Entity(
        TIdentity id,
        DateTime createAt,
        string createBy,
        DateTime lastModified,
        string lastModifiedBy)
    {
        Id = id;
        CreateAt = createAt;
        CreateBy = createBy;
        LastModified = lastModified;
        LastModifiedBy = lastModifiedBy;
    }

    public TIdentity Id { get; protected set; }
    public DateTime CreateAt { get; set; }
    public string CreateBy { get; set; }
    public DateTime LastModified { get; set; }
    public string LastModifiedBy { get; set; }
}
