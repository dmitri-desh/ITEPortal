using ITEPortal.Data.Repositories.Implementation;
using ITEPortal.Data.Repositories.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace ITEPortal.Data
{
    public class DataAccessRegistry
    {
        public static void RegisterRepository(IServiceCollection services)
        {
            services.AddScoped(typeof(IUserRepository), typeof(UserRepository));
            services.AddScoped(typeof(IAuthCodeRepository), typeof(AuthCodeRepository));
        }
    }
}
