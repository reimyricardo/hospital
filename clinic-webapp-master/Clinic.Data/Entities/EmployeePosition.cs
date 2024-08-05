using Clinic.Data.Entities.Common;

namespace Clinic.Data.Entities;

public class EmployeePosition : AuditableEntity
{
    public EmployeePosition()
    {
        Employees = new HashSet<Employee>();
    }

    public string PositionName { get; set; } = string.Empty;

    public ICollection<Employee> Employees { get; set; }
}
