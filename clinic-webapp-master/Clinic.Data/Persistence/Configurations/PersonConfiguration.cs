using Clinic.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Clinic.Data.Persistence.Configurations;

public sealed class PersonConfiguration : IEntityTypeConfiguration<Person>
{
    public void Configure(EntityTypeBuilder<Person> builder)
    {
        builder.HasIndex(x => x.NIF).IsUnique();

        builder.HasIndex(x => x.SocialNumber).IsUnique();

        builder.HasQueryFilter(x => !x.IsDeleted);
    }
}
