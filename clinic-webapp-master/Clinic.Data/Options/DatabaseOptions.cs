﻿namespace Clinic.Data.Options;

public class DatabaseOptions 
{
    public const string sectionName = "DatabaseOptions";

    public string? ConnectionString { get; set; }

    public int MaxRetryCount { get; set; }

    public int CommandTimeout { get; set; }

    public bool EnableDetailedErrors { get; set; }

    public bool EnableSensitiveDataLogging { get; set; }
}
