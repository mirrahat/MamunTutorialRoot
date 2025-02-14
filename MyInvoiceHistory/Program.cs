var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//var builder = WebApplication.CreateBuilder(args);


builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.myinvoicehistory.json", optional: false, reloadOnChange: true)
    .AddEnvironmentVariables();



builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.MapGet("/", () => "Welcome to MyInvoiceHistory API!");
// Configure the HTTP request pipeline.
// Add Swagger services
app.UseSwagger();
app.UseSwaggerUI();

builder.Services.AddSwaggerGen();
var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
app.Urls.Add($"http://*:{port}");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
