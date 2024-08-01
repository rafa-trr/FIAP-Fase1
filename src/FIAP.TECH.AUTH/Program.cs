using FIAP.TECH.AUTH.Configurations;
using FIAP.TECH.CORE.APPLICATION;
using FIAP.TECH.CORE.APPLICATION.Authentication;
using FIAP.TECH.CORE.APPLICATION.Services.Users;
using FIAP.TECH.CORE.APPLICATION.Settings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add methods extensions
builder.Services.AddInjectionApplication(builder.Configuration);

// Add DbContext
builder.Services.ConfigureDbContext();

builder.Services.Configure<TokenSettings>(builder.Configuration.GetSection("TokenSettings"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("/login", [AllowAnonymous] async ([FromBody] AuthenticateRequest request, IUserService userService) =>
{
    var response = await userService.Authenticate(request);

    if (response is null)
        return Results.BadRequest(new { message = "Email e/ou senha inválido(s)" });

    return Results.Ok(response);
})
.WithName("login")
.WithOpenApi();

app.Run();
