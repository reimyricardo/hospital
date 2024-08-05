using Clinic.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Clinic.Data.Persistence.Configurations;

public sealed class PersonVacationPeriodConfiguration : IEntityTypeConfiguration<PersonVacationPeriod>
{
    public void Configure(EntityTypeBuilder<PersonVacationPeriod> builder)
    {
        builder.HasQueryFilter(x => !x.IsDeleted);
    }
}
