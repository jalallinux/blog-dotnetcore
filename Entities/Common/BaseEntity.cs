using System.ComponentModel.DataAnnotations;

namespace Entities;

// Each entity class implement this interface
// EntityFramework make table for that class
public interface IEntity
{
}

public abstract class BaseEntity<TKey>: IEntity
{
    [Key]
    public TKey Id { get; set; }
}

public abstract class BaseEntity: BaseEntity<int>
{
    [Key]
    public int Id { get; set; }
}