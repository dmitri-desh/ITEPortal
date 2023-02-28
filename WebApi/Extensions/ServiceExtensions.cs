using FluentValidation;
using ITEPortal.Data;
using ITEPortal.Domain;
using ITEPortal.Domain.Dto;
using ITEPortal.Domain.Services.Implementation;
using ITEPortal.Domain.Services.Interfaces;
using ITEPortal.Domain.Validators;
using MessengerService;

namespace WebApi.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });
        }
        public static void ConfigureMessengerService(this IServiceCollection services)
        {
            services.AddSingleton<IEmailManager, EmailManager>();
        }
        public static void ConfigureDataAccessRegistry(this IServiceCollection services)
        {
            DataAccessRegistry.RegisterRepository(services);
            ComponentAccessRegistry.RegisterServices(services);
            AutomapperRegistry.RegisterServices(services);
        }
        public static void ConfigureValidatorService(this IServiceCollection services)
        {
            services.AddScoped<IValidator<UserDto>, UserValidator>();
            services.AddScoped<IValidator<AuthCodeDto>, AuthCodeValidator>();
            services.AddScoped<IValidator<UserRoleDto>, UserRoleValidator>();
        }
        public static void ConfigureJwtService(this IServiceCollection services)
        {
            services.AddSingleton<IJwtService, JwtService>();
            services.AddScoped<ITokenClaimsService, TokenClaimsService>();
        }
    }
}
