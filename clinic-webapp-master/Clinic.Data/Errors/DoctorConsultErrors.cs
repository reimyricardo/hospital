using Clinic.Data.Entities.Common.Primitives;

namespace Clinic.Data.Errors;

public static class DoctorConsultErrors 
{
    public static readonly Error NotFound
        = Error.NotFound("DoctorConsult.NotFound","The doctor consult was not found");
}
