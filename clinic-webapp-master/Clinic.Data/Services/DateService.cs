using Clinic.Data.Contracts;

namespace Clinic.Data.Services;

public class DateService : IDateService
{
    public DateTime NowUTC => DateTime.UtcNow;
}
