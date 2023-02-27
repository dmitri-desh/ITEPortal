using ITEPortal.Data.Models;
using ITEPortal.Data.Persistence;
using ITEPortal.Data.Repositories.Interfaces;

namespace ITEPortal.Data.Repositories.Implementation
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(ApplicationContext context) : base(context)
        {
        }
    }
}
