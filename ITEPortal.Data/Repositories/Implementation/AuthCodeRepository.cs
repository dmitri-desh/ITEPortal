using ITEPortal.Data.Models;
using ITEPortal.Data.Persistence;
using ITEPortal.Data.Repositories.Interfaces;

namespace ITEPortal.Data.Repositories.Implementation
{
    public class AuthCodeRepository : Repository<AuthCode>, IAuthCodeRepository
    {
        public AuthCodeRepository(ApplicationContext context) : base(context)
        {
        }
    }
}
