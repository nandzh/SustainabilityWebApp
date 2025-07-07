using Microsoft.AspNetCore.Authentication.MicrosoftAccount;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SustainabilityWebApp.Components;
using SustainabilityWebApp.Components.Account;
using SustainabilityWebApp.Data;
using Microsoft.Extensions.DependencyInjection;
using Azure.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;


try
{
    var builder = WebApplication.CreateBuilder(args);

    var keyVaultName = "sustainabilityvault";
    var kvUri = new Uri($"https://{keyVaultName}.vault.azure.net/");
    builder.Configuration.AddAzureKeyVault(kvUri, new DefaultAzureCredential());


    var sustainabilityConnectionString = builder.Configuration.GetConnectionString("SustainabilityWebAppContext")
                                         ?? throw new InvalidOperationException(
                                             "Connection string 'SustainabilityWebAppContext' not found.");
    var defaultConnectionString = builder.Configuration.GetConnectionString("DefaultConnection")
                                  ?? throw new InvalidOperationException(
                                      "Connection string 'DefaultConnection' not found.");

    var clientId = builder.Configuration["ClientId"]
                   ?? throw new InvalidOperationException("ClientId not found in configuration.");
    var clientSecret = builder.Configuration["ClientSecret"]
                       ?? throw new InvalidOperationException("ClientSecret not found in configuration.");

    Console.WriteLine($"DefaultConnection from config: {defaultConnectionString}");


    builder.Services.AddDbContextFactory<SustainabilityWebAppContext>(options =>
        options.UseSqlServer(sustainabilityConnectionString));

    builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlServer(defaultConnectionString));

    builder.Services.AddDatabaseDeveloperPageExceptionFilter();

    builder.Services.AddQuickGridEntityFrameworkAdapter();

    builder.Services.AddRazorComponents()
        .AddInteractiveServerComponents();

    builder.Services.AddCascadingAuthenticationState();

    builder.Services.AddScoped<IdentityUserAccessor>();
    builder.Services.AddScoped<IdentityRedirectManager>();
    builder.Services.AddScoped<AuthenticationStateProvider, IdentityRevalidatingAuthenticationStateProvider>();

    builder.Services.AddAuthentication(options =>
        {
            options.DefaultScheme = IdentityConstants.ApplicationScheme;
            options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
        })
        .AddMicrosoftAccount(microsoftOptions =>
        {
            microsoftOptions.ClientId = clientId;
            microsoftOptions.ClientSecret = clientSecret;
        })
        .AddIdentityCookies();

    builder.Services.AddIdentityCore<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
        .AddEntityFrameworkStores<ApplicationDbContext>()
        .AddSignInManager()
        .AddDefaultTokenProviders();

    builder.Services.AddSingleton<IEmailSender<ApplicationUser>, IdentityNoOpEmailSender>();

    var app = builder.Build();

    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;

        //SeedData.SeedFromExcel(app.Services, "Data/Sustainability_rates.xlsx");
    }

    if (app.Environment.IsDevelopment())
    {
        app.UseMigrationsEndPoint();
    }
    else
    {
        app.UseExceptionHandler("/Error", createScopeForErrors: true);
        app.UseHsts();
        app.UseMigrationsEndPoint();
    }

    app.UseHttpsRedirection();

    app.UseAntiforgery();

    app.MapStaticAssets();

    app.MapRazorComponents<App>()
        .AddInteractiveServerRenderMode();

    app.MapAdditionalIdentityEndpoints();

    app.Run();
}
catch (Exception ex)
{
    File.WriteAllText("/home/LogStartupError.txt", ex.ToString()); // Linux path for Azure App Service
    throw; // Rethrow so Azure shows 500.30
}
