using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITEPortal.Data
{
    public class DataAccessRegistry
    {
        public static void RegisterRepository(IServiceCollection services)
        {
            //services.AddScoped(typeof(IApplicantRepository), typeof(ApplicantRepository));
        }
    }
}
