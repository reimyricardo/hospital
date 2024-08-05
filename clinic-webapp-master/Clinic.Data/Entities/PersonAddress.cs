using Clinic.Data.Entities.Common;

namespace Clinic.Data.Entities;

public class PersonAddress : AuditableEntity
{
    public int StreerNumber { get; set; }

    public string AddressLine1 { get; set; } = string.Empty;

    public string AddressLine2 { get; set; } = string.Empty;

    public string City { get; set; } = string.Empty;

    public int PostalCode { get; set; }

    public int Population { get; set; }

    public string Province { get; set; } = string.Empty;

    public int PersonId { get; set; }

    public Person Person { get; set; } = new();
}
