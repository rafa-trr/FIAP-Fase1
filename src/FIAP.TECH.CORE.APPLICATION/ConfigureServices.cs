﻿using FIAP.TECH.CORE.APPLICATION.Services.Users;
using FIAP.TECH.CORE.DOMAIN.Interfaces.Repositories;
using FIAP.TECH.INFRASTRUCTURE.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace FIAP.TECH.CORE.APPLICATION;

public static class ConfigureServices
{
    public static void AddInjectionApplication(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        // Repositories
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IContactRepository, ContactRepository>();
        services.AddScoped<IRegionRepository, RegionRepository>();

        // Services
        services.AddScoped<IUserService, UserService>();
    }
}
