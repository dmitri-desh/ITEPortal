using ITEPortal.Domain.Services.Implementation;
using ITEPortal.Domain.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace ITEPortal.Domain
{
    public class ComponentAccessRegistry
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped(typeof(IUserService), typeof(UserService));
            services.AddScoped(typeof(IAuthCodeService), typeof(AuthCodeService));
        }
    }
}
