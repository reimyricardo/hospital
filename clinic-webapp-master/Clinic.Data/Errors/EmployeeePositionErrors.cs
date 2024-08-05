using Clinic.Data.Entities.Common.Primitives;

namespace Clinic.Data.Errors
{
    public static class EmployeePositionErrors
    {
        public static Error NotFoundByPositionName(string employeePosition)
            => Error.NotFound("EmployeePosition.NotFoundByPositionName", $"The employee position with the name of {employeePosition} was not found");
    }
}
