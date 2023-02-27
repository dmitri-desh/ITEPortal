using ITEPortal.Data.Models;
using ITEPortal.Data.Persistence;
using ITEPortal.Data.Repositories.Interfaces;

namespace ITEPortal.Data.Repositories.Implementation
{
    public class UserRoleRepository : Repository<UserRole>, IUserRoleRepository
    {
        public UserRoleRepository(ApplicationContext context) : base(context)
        {
        }
    }
}
