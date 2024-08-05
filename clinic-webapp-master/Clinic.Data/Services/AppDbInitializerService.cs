using Clinic.Data.Contracts;
using Clinic.Data.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Clinic.Data.Services;

public class AppDbInitializerService : IAppDbInitializerService
{
    private readonly AppDbContext _appDbContext;
    private readonly ILogger<AppDbInitializerService> _logger;
    private readonly IVacationPeriodStatus _vacationPeriodStatus;
    private readonly IDoctorPositionRepository _doctorPositionRepository;
    private readonly IPatientRepository _patientRepository;
    private readonly IEmployeePositionRepository _employeePositionRepository;

    public AppDbInitializerService(AppDbContext appDbContext,
                            ILogger<AppDbInitializerService> logger,
                            IVacationPeriodStatus vacationPeriodStatus,
                            IDoctorPositionRepository doctorPositionRepository,
                            IEmployeePositionRepository employeePositionRepository,
                            IPatientRepository patientRepository)
    {
        _appDbContext = appDbContext;
        _logger = logger;
        _vacationPeriodStatus = vacationPeriodStatus;
        _doctorPositionRepository = doctorPositionRepository;
        _employeePositionRepository = employeePositionRepository;
        _patientRepository = patientRepository;
    }
    public async Task ConnectAsync()
    {
        try
        {
            await _appDbContext.Database.CanConnectAsync();

        }
        catch (Exception ex)
        {
            _logger.LogError("An error occured trying to connect to the database with the provider name {databaseProviderName} : {message}", _appDbContext.Database.ProviderName, ex.Message);

            throw;
        }
    }

    public async Task MigrateAsync()
    {
        try
        {
            await _appDbContext.Database.MigrateAsync();

        }
        catch (Exception ex)
        {
            _logger.LogError("An error occured trying to do migration to the database with the provider name {databaseProviderName} : {message}", _appDbContext.Database.ProviderName, ex.Message);
        }
    }

    public async Task SeedAsync()
    {
        try
        {
            await TrySeedEmployeePositionAsync();

            await TrySeedDoctorPositionAsync();

            await TrySeedVacationPeriodStatusAsync();

        }
        catch (Exception ex)
        {
            _logger.LogError("An error occurred trying to seed the database with the error message : {message}", ex.Message);

            throw;
        }
    }

    private async Task TrySeedVacationPeriodStatusAsync()
    {
        if (!_appDbContext.VacationPeriodStatus.Any())
        {
            await _vacationPeriodStatus.AddDefaultVacationPeriodStatus();
        }
    }

    private async Task TrySeedDoctorPositionAsync()
    {
        if (!_appDbContext.DoctorPosition.Any())
        {
            await _doctorPositionRepository.AddDefaultDoctorPosition();
        }
    }

    private async Task TrySeedEmployeePositionAsync()
    {
        if (!_appDbContext.EmployeePosition.Any())
        {
            await _employeePositionRepository.AddDefaultEmployeePosition();
        }
    }
}
