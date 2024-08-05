using Clinic.Data.Options;
using Clinic.Data.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using FluentValidation;
using System.Reflection;
using Clinic.Data.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Clinic.Data.Contracts;
using Clinic.Data.Repositories;
using Clinic.Data.Common;
using Clinic.Data.Persistence.Interceptors;
using Clinic.Data.Validators;

namespace Clinic.Data;

public static class DataServiceRegistration
{
    public static IServiceCollection AddDataLayer(this IServiceCollection services, IWebHostEnvironment hostEnvironment)
    {
        services.AddOptions<DatabaseOptions>()
                .BindConfiguration(DatabaseOptions.sectionName);

        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        services.AddSingleton<IValidateOptions<DatabaseOptions>, DatabaseOptionsValidator>();

        services.AddSingleton<AuditableEntititesInterceptor>();

        services.AddSingleton<SoftDeleteEntitiesInterceptor>();

        services.AddDbContext<AppDbContext>((serviceProvider, options) =>
        {
            DatabaseOptions databaseOptions = serviceProvider.GetRequiredService<IOptions<DatabaseOptions>>().Value;

            options.UseMySql(databaseOptions.ConnectionString, ServerVersion.AutoDetect(databaseOptions.ConnectionString), sqlServerOptionsAction =>
            {
                sqlServerOptionsAction.CommandTimeout(databaseOptions.CommandTimeout);

                sqlServerOptionsAction.EnableRetryOnFailure(databaseOptions.MaxRetryCount);
            })
            .AddInterceptors(serviceProvider.GetRequiredService<AuditableEntititesInterceptor>())
            .AddInterceptors(serviceProvider.GetRequiredService<SoftDeleteEntitiesInterceptor>());

            if (hostEnvironment.IsDevelopment())
            {
                options.EnableDetailedErrors(databaseOptions.EnableDetailedErrors);
                options.EnableSensitiveDataLogging(databaseOptions.EnableSensitiveDataLogging);
            }
        });

        // Unit of work
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        // Services
        services.AddTransient<IDateService, DateService>();
        services.AddScoped<IAppDbInitializerService, AppDbInitializerService>();

        // Repositories
        services.AddScoped<IVacationPeriodStatus, VacationPeriodStatusRepository>();
        services.AddScoped<IDoctorPositionRepository, DoctorPositionRepository>();
        services.AddScoped<IEmployeePositionRepository, EmployeePositionRepository>();
        services.AddScoped<IDoctorRepository, DoctorRepository>();
        services.AddScoped<IPersonRepository, PersonRepository>();
        services.AddScoped<IPatientRepository, PatientRepository>();
        services.AddScoped<IDoctorConsultRepository, DoctorConsultRepository>();
        services.AddScoped<IPersonAddressRepository, PersonAddressRepository>();
        services.AddScoped<IDoctorPatientRepository, DoctorPatientRepository>();
        services.AddScoped<IPersonVacationPeriodRepository, PersonVacationPeriodRepository>();
        services.AddScoped<IEmployeeRepository, EmployeeRepository>();
        services.AddScoped<IEmployeePositionRepository, EmployeePositionRepository>();

        return services;
    }

    public static async Task InitializeDatabaseAsync(this IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope(); 

        IAppDbInitializerService initializer = scope.ServiceProvider.GetRequiredService<IAppDbInitializerService>();

        await initializer.ConnectAsync();

        await initializer.MigrateAsync();

        await initializer.SeedAsync();
    }
}
