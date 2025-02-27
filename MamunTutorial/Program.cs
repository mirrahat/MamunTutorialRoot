using MamunTutorial.Data;
using MamunTutorial.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(new WebApplicationOptions
{
    WebRootPath = "wwwroot"
});

// ✅ Detect if running inside Docker
var isDocker = Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER") == "true";

// ✅ Read ports from environment variables (useful for Docker & local)
var httpPort = builder.Configuration["ASPNETCORE_HTTP_PORT"] ?? "5276";
var httpsPort = builder.Configuration["ASPNETCORE_HTTPS_PORT"] ?? "7287";

// ✅ Ensure the app runs on the correct ports
builder.WebHost.ConfigureKestrel(options =>
{
    if (isDocker)
    {
        options.ListenAnyIP(8080); // Docker (containerized)
    }
    else
    {
        options.ListenAnyIP(int.Parse(httpPort)); // Use HTTP from config
        options.ListenAnyIP(int.Parse(httpsPort), listenOptions => listenOptions.UseHttps()); // Use HTTPS from config
    }
});

// ✅ Add services to the container
builder.Services.AddControllers(options =>
{
    options.Filters.Add(new ProducesAttribute("application/json"));
    options.Filters.Add(new ConsumesAttribute("application/json"));

    var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
    options.Filters.Add(new AuthorizeFilter(policy));
}).AddXmlSerializerFormatters();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ✅ Add DbContext with SQL Server
builder.Services.AddDbContext<ApplicationDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// ✅ Configure CORS
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("http://localhost:3000")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

// ✅ Enable detailed security logs
IdentityModelEventSource.ShowPII = true;

// ✅ Add authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    // Ensure the JWT key is not null or empty
    var jwtKey = builder.Configuration["Jwt:Key"];
    if (string.IsNullOrEmpty(jwtKey))
    {
        throw new InvalidOperationException("JWT Key is not configured in the application settings.");
    }

    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateAudience = true,
        ValidAudience = builder.Configuration["Jwt:Audience"],
        ValidateIssuer = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
    };

    options.Events = new JwtBearerEvents
    {
        OnAuthenticationFailed = context =>
        {
            Console.WriteLine($"Authentication failed: {context.Exception.Message}");
            return Task.CompletedTask;
        },
        OnTokenValidated = context =>
        {
            Console.WriteLine("Token is valid.");
            return Task.CompletedTask;
        },
        OnChallenge = context =>
        {
            Console.WriteLine($"Challenge triggered: {context.Error}, {context.ErrorDescription}");
            return Task.CompletedTask;
        }
    };
});

// ✅ Add authorization
builder.Services.AddAuthorization();

// ✅ Register custom services
builder.Services.AddScoped<AuthService>();
builder.Services.AddTransient<IJwtService, JwtService>();

var app = builder.Build();

// ✅ Middleware setup
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/error"); // Better error handling for production
}

app.UseStaticFiles();
app.UseRouting();
app.UseCors();
app.UseAuthentication();
app.UseAuthorization();

// ✅ Add Swagger (API Documentation)
app.UseSwagger();
app.UseSwaggerUI();

// ✅ Define routes
app.MapControllers();
app.MapGet("/", () => "Hello, Docker!").AllowAnonymous();

// ✅ Run the application
app.Run();
