using Clinic.Data.Entities.Common.Primitives;

namespace Clinic.Data.Errors;

public static class PersonAddressErrors 
{
    public static Error NotFoundByAddressIdAndNIF(int addressId, string nif)
        => Error.NotFound("PersonAddress.NotFound",$"The person address with the id of {addressId} and the person nif of {nif} was not found");

    public static Error NotFoundById(int addressId)
        => Error.NotFound("PersonAddres.NotFound",$"The peson address with the id of {addressId} was not found");
}
