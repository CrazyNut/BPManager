using API.ProcessExecutor.Interfaces;
using API.Data;
using API.Data.Repositories;
using API.DTO;
using API.Entities;
using API.Helpers;
using API.Interfaces;
using API.Services;
using API.UnitsOfWork;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;


// Add services to the container.
builder.Services.AddScoped<IProcessElementTypesService, ProcessElementTypesService>();
builder.Services.AddScoped<IProcessExecutorService, ProcessExecutorService>();
builder.Services.AddScoped<IMessagerService, InFileMessagerService>();

builder.Services.AddScoped<IRepository<ProcessEntity>, ProcessRepository>();
builder.Services.AddScoped<IRepository<ProcessElementEntity>, ProcessElementRepository>();
builder.Services.AddScoped<IRepository<ProcessElementConnectionEntity>, ProcessElementConnectionRepository>();

builder.Services.AddScoped<AppUnitOfWork>();

builder.Services.AddScoped<IEntityService<ProcessDTO>, ProcessService>();

builder.Services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);

builder.Services.AddDbContext<ApplicationContext>(options =>
{
    options.UseSqlite(configuration.GetConnectionString("DefaultConnection"));
    options.EnableSensitiveDataLogging();
});



builder.Services.AddLocalization(options =>
{
    options.ResourcesPath = "Resources";
});
var supportedCultures = new[]
{
    new CultureInfo("ru"),
    new CultureInfo("en")
};
builder.Services.Configure<RequestLocalizationOptions>(options => {
    options.DefaultRequestCulture = new RequestCulture("ru");
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
});

builder.Services.AddControllers();
builder.Services.AddCors();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRequestLocalization(app.Services.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value);

app.UseHttpsRedirection();

app.UseCors(policy => policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:4200"));

app.UseAuthorization();

app.MapControllers();

app.Run();
