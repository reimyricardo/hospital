using Clinic.Data.Entities.Common.Primitives;

namespace Clinic.Data.Errors;

public static class PersonErrors
{
    public static readonly Error NifNotUnique 
        = Error.Conflit("Person.NifNotUnique","The NIF is already register");

    public static readonly Error SocialNumberNotUnique
        = Error.Conflit("Person.SocialNumber", "The Social Number is already register");

    public static Error NotFoundByNameAndNif(string name, string nif)
        => Error.NotFound("Person.NotFound",$"The person with the name of {name} and NIF of {nif} was not found");

    public static Error NotFoundById(int id)
        => Error.NotFound("Person.NotFound", $"The person with the id of {id} was not found");
}
