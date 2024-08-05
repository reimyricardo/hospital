namespace Clinic.Data.DTOs;

public class PatientResponse
{
    public int PatientId { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Telephone { get; set; } = string.Empty;

    public string NIF { get; set; } = string.Empty;

    public int SocialNumber { get; set; }
}

