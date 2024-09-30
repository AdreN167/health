using Health.Api;
using Health.Api.Extensions;
using Health.Core;
using Health.Core.Features.Chat.Hubs;
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
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials()
            .SetIsOriginAllowed(_ => true);
    });
});

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.ApplyMigrations();
}

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.UseStaticFiles();

app.MapHub<ChatHub>("/chat");

app.Run();
