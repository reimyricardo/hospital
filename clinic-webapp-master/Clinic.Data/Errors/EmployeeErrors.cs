using Clinic.Data.Entities.Common.Primitives;

namespace Clinic.Data.Errors
{
    public class EmployeeErrors
    {
        public static Error NotFoundById(int employeeId)
            => Error.NotFound("Employee.NotFoundById", $"The employee with the id of {employeeId} was not found");

        public static Error EmployeeNumberNotUnique(int employeeNumber)
            => Error.Conflit("Employee.EmployeeNumberConflict", $"The employee number {employeeNumber} already exists");

        public static Error NotFoundByName(string employeeName)
            => Error.NotFound("Employee.NotFoundByName", $"The employee with the name of {employeeName} was not found");

        public static Error NotFoundByNameAndEmployeeNumber(string employeeName, int employeeNumber)
            => Error.NotFound("Employee.NotFoundByNameAndEmployeeNumber", $"The employee with the name of {employeeName} and employee number {employeeNumber} was not found");
    }
}

