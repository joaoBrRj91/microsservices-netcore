namespace Ordering.Domain.Abstractions;

public interface IEntity
{
    public DateTime CreateAt { get; set; }
    public string CreateBy { get; set; }
    public DateTime LastModified { get; set; }
    public string LastModifiedBy { get; set; }
}
