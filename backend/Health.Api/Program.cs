using Health.Api;
using Health.Core;
using Health.DAL;
using Health.Domain.Models.Settings;

var builder = WebApplication.CreateBuilder(args);

// Вытаскиваем данные про jwt из appsettings.json
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection(JwtSettings.DefaultSection));

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddAuthenticationAndAuthorization(builder);
builder.Services.AddSwagger();

builder.Services.AddDAL(builder.Configuration);
builder.Services.AddCore();

// CORS
builder.Services.AddCors();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(builder => builder.AllowAnyOrigin()
                              .AllowAnyMethod()
                              .AllowAnyHeader()
                              .SetIsOriginAllowed(_ => true));

app.UseAuthorization();

app.MapControllers();

app.Run();
