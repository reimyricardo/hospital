namespace Clinic.Data.Contracts;

public interface IAppDbInitializerService
{
    Task ConnectAsync();

    Task MigrateAsync();

    Task SeedAsync();
}
