using Clinic.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Clinic.Data.Persistence.Configurations;

public sealed class DoctorConsultConfiguration : IEntityTypeConfiguration<DoctorConsult>
{
    public void Configure(EntityTypeBuilder<DoctorConsult> builder)
    {
        builder.HasQueryFilter(x => !x.IsDeleted);
    }
}
