using System;
using AutoMapper;
using Blog.DAL.ApplicationContext;
using Blog.DAL.Entities;
using Blog.DAL.Interfaces;
using Blog.DAL.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Blog.BLL.Configurations
{
	public static class ServicesBLLModule
	{
        /// <summary>
        /// Automapper + repositories
        /// </summary>
        /// <param name="services"></param>
        /// <param name="mappingProfile" - профиль automapper со столя клиента (DTO <-> VM)></param>
        /// <returns></returns>
        public static IServiceCollection AddBllServices(this IServiceCollection services, Profile profile = null)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            var mapperConfig = new MapperConfiguration((v) =>
            {
                if (profile != null)
                    v.AddProfile(profile);

                v.AddProfile(new MappingProfileBLL());
            });

            IMapper mapper = mapperConfig.CreateMapper();

            services.AddSingleton(mapper);

            return services;
        }

        public static IServiceCollection AddSqlLiteContext(this IServiceCollection services, string connectionString = null)
        {

            if(connectionString == null)
            {
                connectionString = "DataSource=../Blog.DAL/Database/blog_database.db";
            }

            services.AddDbContext<DataContext>(
                options => options.UseSqlite(connectionString,
                opts => opts.MigrationsAssembly("Blog.DAL"))
                .EnableSensitiveDataLogging())
                .AddIdentity<UserAccount, Role>(options =>
                {
                    options.Password.RequiredLength = 5;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireDigit = false;
                })
                .AddEntityFrameworkStores<DataContext>();

            return services;
        }
    }
}

