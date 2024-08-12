using FIAP.TECH.API.Configurations;
using FIAP.TECH.CORE.APPLICATION;
using FIAP.TECH.CORE.APPLICATION.Settings.JwtExtensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Add Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerConfiguration();

// Add methods extensions
builder.Services.AddInjectionApplication(builder.Configuration);

builder.Services.Configure<TokenSettings>(builder.Configuration.GetSection("TokenSettings"));
builder.Services.AddSecurity();

// Add DbContext
builder.Services.AddDbContextConfiguration();

var app = builder.Build();

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
