using Dapper;
using HospitalApi;
using HospitalApi.Connection;
using HospitalApi.Extensions;
using HospitalApi.Repositories;
using HospitalApi.Scripts;
using Microsoft.Data.SqlClient;
using System.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddApplicationServices();
builder.Services.AddRepositories();
builder.Services.AddIdentityServices(builder.Configuration);
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

app.UseHttpsRedirection();

app.UseCors(p => p.AllowAnyHeader()
                  .AllowAnyMethod()
                  .AllowAnyOrigin());

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
try
{
    var hospitalConn = new HospitalConnection(builder.Configuration);
    if (hospitalConn is not null)
    {
        using (IDbConnection connection = new SqlConnection(hospitalConn.ConnectionString))
        {
            string exist = await connection.ExecuteScalarAsync<string>(DatabaseScripts.ExistDatabase);
            if (string.IsNullOrEmpty(exist))
            {
                string dbCreation = File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), @"Scripts\CreateDatabase.txt"));
                await connection.ExecuteAsync(dbCreation);
            }

            await Seed.SeedRoles(services.GetRequiredService<RoleRepository>());
            await Seed.SeedHospitals(services.GetRequiredService<HospitalRepository>());
            await Seed.SeedUsers(
                services.GetRequiredService<UserRepository>(),
                services.GetRequiredService<HospitalRepository>());
        }
    }
}
catch (Exception ex)
{
    var logger = services.GetService<ILogger<Program>>();
    logger.LogError(ex, "An error occurred during migration");
}

app.Run();
