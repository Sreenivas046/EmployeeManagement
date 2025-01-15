using EmployeeManagement.API.Services;
using EmployeeManagement.DATA.Data;
using EmployeeManagement.DATA.Services;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddScoped<IEmployeeRepo, EmployeeRepo>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();

// DbContext with SQL Server
builder.Services.AddDbContext<EmployeeDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("EmployeeConnection"),
    options => options.EnableRetryOnFailure(
       maxRetryCount: 5, // Number of retry attempts
       maxRetryDelay: TimeSpan.FromSeconds(10), // Maximum delay between retries
       errorNumbersToAdd: null // Add specific SQL error codes if needed
   )));

////DbContext with Inmemory
//builder.Services.AddDbContext<EmployeeDbContext>(options =>
//                    options.UseInMemoryDatabase("EmployeeDb"));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
    options.SwaggerDoc("v2", new OpenApiInfo { Title = "My API", Version = "v2" });

    options.ResolveConflictingActions(apiDescriptions =>
    {
        // Choose the first action or implement custom logic to resolve conflicts
        return apiDescriptions.First();
    });
});
builder.Services.AddApiVersioning(options =>
{
    // Report API versions in the response headers
    options.ReportApiVersions = true;

    // Default version (if none is specified)
    options.DefaultApiVersion = new ApiVersion(1, 0);

    // Assume default version when not explicitly specified
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ApiVersionReader = ApiVersionReader.Combine(
        new UrlSegmentApiVersionReader(),  //support url versioning
        new HeaderApiVersionReader("x-api-version"));    // Support x-api-version header
});
builder.Services.AddVersionedApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV";
    options.SubstituteApiVersionInUrl = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1");
        options.SwaggerEndpoint("/swagger/v2/swagger.json", "API v2");

        // Optional: Default to version 1 on Swagger UI load
        options.DefaultModelsExpandDepth(-1);
    });
}
//using (var scope = app.Services.CreateScope())
//{
//    var dbContext = scope.ServiceProvider.GetRequiredService<EmployeeDbContext>();
//    EmployeeDataSeeder.Seed(dbContext);
//}

app.Use(async (context, next) =>
{

    await next.Invoke();
});
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
