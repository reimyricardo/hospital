namespace Clinic.Data.DTOs;

public class PersonAddressResponse
{
    public int AddressId { get; set; }

    public int StreerNumber { get; set; }

    public string AddressLine1 { get; set; } = string.Empty;

    public string AddressLine2 { get; set; } = string.Empty;

    public string City { get; set; } = string.Empty;

    public int PostalCode { get; set; }

    public int Population { get; set; }

    public string Province { get; set; } = string.Empty;

    public string PersonName { get; set; } = string.Empty;
}
