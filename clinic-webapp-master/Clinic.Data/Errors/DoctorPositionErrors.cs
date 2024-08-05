using Clinic.Data.Entities.Common.Primitives;

namespace Clinic.Data.Errors;

public static class DoctorPositionErrors
{
    public static Error NotFoundByPositionName(string doctorPosition) 
        => Error.NotFound("DoctorPosition.NotFoundByPositionName",$"The doctor position name with the name of {doctorPosition} was not found");
}
