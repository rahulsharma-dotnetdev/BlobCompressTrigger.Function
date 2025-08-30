var host = new HostBuilder()
    .ConfigureFunctionsWebApplication()
    .ConfigureAppConfiguration((context, config) =>
    {
        var builtConfig = config.Build();
        var keyVaultUri = builtConfig["KeyVaultUri"];
        if (!string.IsNullOrEmpty(keyVaultUri))
        {
            config.AddAzureKeyVault(new Uri(keyVaultUri), new DefaultAzureCredential());
        }
    })
    .ConfigureServices((context, services) =>
    {
        var configuration = context.Configuration;

        var blobConnectionString = configuration["AzureWebJobsStorage"] ?? throw new Exception("Blob connection string is missing");

        services.AddSingleton(new BlobServiceClient(blobConnectionString));
        services.AddSingleton<ImageHelper>();

        // Application Insights
        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();
    })
    .Build();

host.Run();