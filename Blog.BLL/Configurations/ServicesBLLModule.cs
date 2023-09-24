using System;
using Blog.DAL.ApplicationContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Blog.BLL.Configurations
{
	public static class ServicesBLLModule
	{
		public static IServiceCollection AddBllServices(this IServiceCollection services)
		{

			return services;
		}

        // Пример реализации разных вариантов подключений. Нужны соответветсвующие NuGet пакеты 

        //public static IServiceCollection AddSqlServerContext(this IServiceCollection services, string connectionString)
        //{
        //	services.AddDbContext<DbContext>(
        //	    options => options.UseNpgsql(connectionString,
        //      opts => opts.MigrationsAssembly("Blog.WebClient")));
        //	return services;
        //}

        //public static IServiceCollection AddNpgsqlContext(this IServiceCollection services, string connectionString)
        //{
        //	services.AddDbContext<DbContext>(
        //	    options => options.UseNpgsql(connectionString,
        //      opts => opts.MigrationsAssembly("Blog.WebClient")));

        //	return services;
        //}

        public static IServiceCollection AddSqlLiteContext(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<DataContext>(
				options => options.UseSqlite(connectionString,
                opts => opts.MigrationsAssembly("Blog.WebClient")));

            return services;
        }
    }
}

