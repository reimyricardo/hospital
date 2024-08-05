namespace Clinic.Data.Entities.Common;

public abstract class AuditableEntity : BaseEntity
{
    public DateTime? CreatedAt { get; set; }

    public DateTime? ModifiedAt { get; set; }
}
