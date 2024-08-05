using Clinic.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Clinic.Data.Persistence;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)  { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Aplying the configuration of the entities from the current Assembly
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);
    }
    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        // Better to use datetime2 than datetime columntype
        configurationBuilder.Properties<DateTime>().HaveColumnType("datetime2");

        base.ConfigureConventions(configurationBuilder);
    }

    public DbSet<Person> Person => Set<Person>();

    public DbSet<PersonAddress> PersonAddress => Set<PersonAddress>();

    public DbSet<PersonVacationPeriod> PersonVacationPeriod => Set<PersonVacationPeriod>();

    public DbSet<VacationPeriodStatus> VacationPeriodStatus => Set<VacationPeriodStatus>();

    public DbSet<Doctor> Doctor => Set<Doctor>();

    public DbSet<DoctorPosition> DoctorPosition => Set<DoctorPosition>();

    public DbSet<DoctorConsult> DoctorConsult => Set<DoctorConsult>();

    public DbSet<DoctorPatient> DoctorPatient => Set<DoctorPatient>();

    public DbSet<Employee> Employee => Set<Employee>();

    public DbSet<EmployeePosition> EmployeePosition => Set<EmployeePosition>();

    public DbSet<Patient> Patient => Set<Patient>();
}
