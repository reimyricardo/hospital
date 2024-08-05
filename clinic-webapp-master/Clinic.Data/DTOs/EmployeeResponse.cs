namespace Clinic.Data.DTOs;

public class EmployeeResponse
{
    public int EmployeeId { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Telephone { get; set; } = string.Empty;

    public string NIF { get; set; } = string.Empty;

    public int SocialNumber { get; set; }

    public string PositionName { get; set; } = string.Empty;
}

