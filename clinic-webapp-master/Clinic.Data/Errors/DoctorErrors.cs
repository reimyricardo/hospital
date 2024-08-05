using Clinic.Data.Entities.Common.Primitives;


namespace Clinic.Data.Errors
{
    public class DoctorErrors
    {
        public static Error NotFoundById(int doctorId) 
            => Error.NotFound("Doctor.NotFoundById",$"The doctor with the id of {doctorId} was not found");

        public static Error CollegueNumberNotUnique(int collegueNumber)
            => Error.Conflit("Doctor.CollegueNumberConflit",$"The doctor collegue number {collegueNumber} already exists");

        public static Error NotFoundByName(string doctorName)
            => Error.NotFound("Doctor.NotFoundById", $"The doctor with the name of {doctorName} was not found");

        public static Error NotFoundByNameAndCollegueNumber(string doctorName,int collegueNumber)
            => Error.NotFound("Doctor.NotFoundById", $"The doctor with the name of {doctorName} and collegue number {collegueNumber} was not found");
    }
}
