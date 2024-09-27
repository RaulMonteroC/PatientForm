using PatientForm.Api.Extensions;
using PatientForm.Api.Middlewares;
using PatientForm.Application.Extensions;
using PatientForm.Domain.Extensions;
using PatientForm.Infrastructure.Extensions;
using Serilog;

try
{
    var builder = WebApplication.CreateBuilder(args);


    builder.AddPresentation();
    builder.Services.AddControllers();
    builder.Services.AddInfrastructure(builder.Configuration);
    builder.Services.AddDomain();
    builder.Services.AddApplication();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    builder.Services.AddScoped<ErrorHandlingMiddleware>();

    var app = builder.Build();

    app.UseMiddleware<ErrorHandlingMiddleware>();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.MapControllers();

    app.UseHttpsRedirection();

    app.Run();
}
catch (Exception e)
{
    Log.Fatal(e, "Application startup failed");
}
finally
{
    await Log.CloseAndFlushAsync();
}