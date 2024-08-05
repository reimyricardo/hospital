namespace Clinic.Data.DTOs;

public class DoctorConsultResponse
{
    public int ConsultId { get; set; }

    public string DoctorName { get; set; } = string.Empty;

    public string ConsultDate { get; set; } = string.Empty;

    public string ConsultHour { get; set; } = string.Empty;
}
