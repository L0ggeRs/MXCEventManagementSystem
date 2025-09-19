using MXC.Application.Services.EventManagementService;
using MXC.Application.Validators.EventManagement;
using MXC.Infrastructure;
using MXC.Infrastructure.Context;
using MXC.Infrastructure.Services.DateTimeService;
using MXC.WebApi.Middlewares;
using MXC.WebApi.Services.EventManagementService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHttpContextAccessor();
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddSingleton<IDateTimeService, DateTimeService>();
builder.Services.AddScoped<IEventManagementValidators, EventManagementValidators>();
builder.Services.AddScoped<IEventManagementService, EventManagementService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
    options.IgnoreObsoleteActions();
    options.IgnoreObsoleteProperties();
    options.CustomSchemaIds(type => type.FullName);
});

var app = builder.Build();

app.UseMiddleware<ExceptionHandlerMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    using var scope = app.Services.CreateScope();
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationTrackingDbContext>();
    await dbContext.Database.EnsureDeletedAsync();
    await dbContext.Database.EnsureCreatedAsync();
}

app.UseHttpsRedirection();
app.MapControllers();

await app.RunAsync();