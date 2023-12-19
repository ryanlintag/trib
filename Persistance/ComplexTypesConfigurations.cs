using Domain.Users;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Persistance
{
    internal static class ComplexTypesConfigurations
    {

        internal static void RegisterUserComplexTypeProperties(this ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.ComplexProperties<Email>();
            configurationBuilder.ComplexProperties<UpdatedByUser>();
            configurationBuilder.ComplexProperties<Name>();
            configurationBuilder.ComplexProperties<Role>();
        }
    }
}
