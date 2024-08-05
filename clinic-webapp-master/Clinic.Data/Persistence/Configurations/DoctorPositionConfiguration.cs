using Clinic.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Clinic.Data.Persistence.Configurations;

public sealed class DoctorPositionConfiguration : IEntityTypeConfiguration<DoctorPosition>
{
    public void Configure(EntityTypeBuilder<DoctorPosition> builder)
    {
        builder.HasQueryFilter(x => !x.IsDeleted);
    }
}
