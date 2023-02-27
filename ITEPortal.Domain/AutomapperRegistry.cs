using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace ITEPortal.Domain
{
    public class AutomapperRegistry
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
        }
    }
}
