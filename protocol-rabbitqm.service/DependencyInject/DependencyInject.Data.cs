﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using protocol.rabbitqm.data.Data;
using protocol.rabbitqm.shared.Interfaces;

namespace protocol.rabbitqm.service.DependencyInject
{
    public static partial class DependencyInject
    {
        public static IServiceCollection AddServiceData(this IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();
            var config = serviceProvider.GetService<IConfiguration>();
            var connectionstring = config.GetConnectionString("Default");

            services
                .AddDbContext<AppDataContext>((opt) =>
                {
                    opt.UseNpgsql(connectionstring, x => x.MigrationsAssembly("protocol.rabbitqm.data"));
                })
                .AddScoped<IUnitOfWork<AppDataContext>, UnitOfWork<AppDataContext>>()
                ;
            return services;
        }
    }
}
