using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SustainabilityWebApp.Components;
using SustainabilityWebApp.Components.Account;
using SustainabilityWebApp.Data;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Azure.Communication.Email;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
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
    .AddIdentityCookies();
var schemaId = builder.Configuration["AzureAd:SchemaId"];
/*
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("OnlyMe", policy =>
        policy.RequireClaim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier",
            schemaId?? throw new InvalidOperationException("schemaId now found")));
});
*/

var allowedIds = builder.Configuration
    .GetSection("Authorization:AllowedUserIds")
    .Get<List<string>>() ?? throw new InvalidOperationException("AllowedUserIds not found");

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("OnlyMe", policy =>
        policy.RequireAssertion(context =>
        {
            var userId = context.User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")?.Value;
            return userId != null && allowedIds.Contains(userId);
        }));
});



builder.Services.Configure<OpenIdConnectOptions>(OpenIdConnectDefaults.AuthenticationScheme, options =>
{
    options.TokenValidationParameters.RoleClaimType = "roles";
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Admin", policy => policy.RequireRole("Admin"));
});

var azureAdSection = builder.Configuration.GetSection("AzureAd");

builder.Services.AddAuthentication().AddMicrosoftAccount("Microsoft", microsoftOptions =>
{
    var clientId = builder.Configuration["AzureAd:ClientId"];
    var clientSecret = builder.Configuration["AzureAd:ClientSecret"];
    microsoftOptions.ClientId = clientId ?? throw new InvalidOperationException("ClientId is null");
    microsoftOptions.ClientSecret = clientSecret ?? throw new InvalidOperationException("ClientSecret is missing.");
});


var sustainabilityConnectionString = builder.Configuration.GetConnectionString("SustainabilityWebAppContext")
                                     ?? throw new InvalidOperationException("Connection string 'SustainabilityWebAppContext' not found.");

builder.Services.AddDbContextFactory<SustainabilityWebAppContext>(options =>
    options.UseSqlServer(sustainabilityConnectionString));


builder.Services.AddIdentityCore<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<SustainabilityWebAppContext>()
    .AddSignInManager()
    .AddDefaultTokenProviders();
builder.Services.AddQuickGridEntityFrameworkAdapter();


builder.Services.AddSingleton<IEmailSender<ApplicationUser>, IdentityNoOpEmailSender>();

builder.Services.AddHttpContextAccessor();


var app = builder.Build();

/* 
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    SeedData.SeedFromExcel(services, "Data/Sustainability_rates 1.xlsx");
}
*/

if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}


app.UseAuthentication();
app.UseAuthorization();
app.UseAntiforgery();
app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();
app.MapAdditionalIdentityEndpoints();

app.Run();
