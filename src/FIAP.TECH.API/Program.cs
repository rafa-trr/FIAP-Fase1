using FIAP.TECH.API.Configurations;
using FIAP.TECH.CORE.APPLICATION;
using FIAP.TECH.CORE.APPLICATION.Settings.JwtExtensions;
using FIAP.TECH.CORE.APPLICATION.Settings.Swagger;
using FIAP.TECH.INFRASTRUCTURE.Contexts;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Add Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add methods extensions
builder.Services.AddInjectionApplication(builder.Configuration);

builder.Services.Configure<TokenSettings>(builder.Configuration.GetSection("TokenSettings"));
builder.Services.AddSecurity();
builder.Services.AddSwaggerConfgi();


// Add DbContext
builder.Services.ConfigureDbContext();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
