using Asp.Versioning;
using Clinic.Api.ExceptionHandler;
using Clinic.Business;
using Clinic.Data;

var builder = WebApplication.CreateBuilder(args);
{
    // Add services to the container.
    builder.Services.AddDataLayer(builder.Environment);
    builder.Services.AddBusinnessLayer();
    builder.Services.AddControllers();
    builder.Services.AddCors();
    builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
    builder.Services.AddProblemDetails();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddApiVersioning(options =>
    {
        options.DefaultApiVersion = new ApiVersion(1);
        options.ReportApiVersions = true;
        options.AssumeDefaultVersionWhenUnspecified = true;
        options.ApiVersionReader = ApiVersionReader.Combine(
            new UrlSegmentApiVersionReader(),
            new HeaderApiVersionReader("X-Api-Version"));
    }).AddApiExplorer(options =>
    {
        options.GroupNameFormat = "'v'V";
        options.SubstituteApiVersionInUrl = true;
    });
    builder.Services.AddSwaggerGen();
    builder.Configuration.AddUserSecrets<Program>(optional:false,reloadOnChange:true);
}



var app = builder.Build();

{ 
    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();

        app.UseSwaggerUI();

        await app.Services.InitializeDatabaseAsync();
    }

    app.UseHttpsRedirection();

    app.UseExceptionHandler();

    app.UseCors(options => 
    {
        options.AllowAnyHeader();
        options.AllowAnyMethod();
        options.AllowAnyOrigin();
    });

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}

