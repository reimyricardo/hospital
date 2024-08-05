using Clinic.Data.Entities.Common;

namespace Clinic.Data.Entities;

public class Employee : AuditableEntity
{
    public Person Person { get; set; } = new(); // One to One

    public int PersonId { get; set; }

    public int EmployeePositionId { get; set; }

    public EmployeePosition EmployeePosition { get; set; } = new();  // One to One
}
