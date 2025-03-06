namespace Diurn.Core.Common;

public class Entity<T>
{
    public T Id { get; init; }
}

public interface IAuditEntity
{
    string CreatedBy { get; set; }
    DateTime CreatedOn { get; set; }
    string ModifiedBy { get; set; }
    DateTime ModifiedOn { get; set; }
}

public class AuditEntity<T> : Entity<T>, IAuditEntity
{
    public string CreatedBy { get; set; } = "N/A";
    public DateTime CreatedOn { get; set; }
    public string ModifiedBy { get; set; } = "N/A";
    public DateTime ModifiedOn { get; set; }
}