namespace Diurn.Core.Common;

public class Entity
{
    public Guid Id { get; init; }
}

public class AuditEntity
{
    public string CreatedBy { get; set; } = "N/A";
    public DateTime CreatedOn { get; set; }
    public string ModifiedBy { get; set; } = "N/A";
    public DateTime ModifiedOn { get; set; }
}