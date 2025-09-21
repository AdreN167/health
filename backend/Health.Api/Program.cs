using Health.Api;
using Health.Core;
using Health.DAL;
using Health.Domain.Models.Settings;

var builder = WebApplication.CreateBuilder(args);


// Âûòàñêèâàåì äàííûå ïðî jwt èç appsettings.json
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection(JwtSettings.DefaultSection));

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddAuthenticationAndAuthorization(builder);
builder.Services.AddSwagger();

builder.Services.AddDAL(builder.Configuration);
builder.Services.AddCore();


// CORS
builder.Services.AddCors(options => options.AddDefaultPolicy(
    policy => policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));

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

app.Run();
