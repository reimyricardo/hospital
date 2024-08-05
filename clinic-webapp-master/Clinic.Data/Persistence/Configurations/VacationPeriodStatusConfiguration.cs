using Clinic.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Clinic.Data.Persistence.Configurations;

public class VacationPeriodStatusConfiguration : IEntityTypeConfiguration<VacationPeriodStatus>
{
    public void Configure(EntityTypeBuilder<VacationPeriodStatus> builder)
    {
        builder.HasQueryFilter(x => !x.IsDeleted);
    }
}
