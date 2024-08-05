namespace Clinic.Data.Entities.Common;

public abstract class SoftEntity
{
    public bool IsDeleted { get; set; }

    public DateTime DeletedAt { get; set; } = new DateTime(1999, 01, 01, 0, 0, 0, DateTimeKind.Unspecified);
}
