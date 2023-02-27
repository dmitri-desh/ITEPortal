using ITEPortal.Data.Models;

namespace ITEPortal.Data.Persistence
{
    public class DbInitializer
    {
        public static void Initialize(ApplicationContext context)
        {
            context.Database.EnsureCreated();

            // Look for any roles.
            if (context.UserRoles.Any())
            {
                return;   // DB has been seeded
            }

            var roles = new UserRole[]
            {
                new UserRole { Name = "Супер-Админ", GlobalSettingsAllowed = true },
                new UserRole { Name = "Администратор", GlobalSettingsAllowed = false },
                new UserRole { Name = "Менеджер поддержки", GlobalSettingsAllowed = false },
                new UserRole { Name = "Технический менеджер", GlobalSettingsAllowed = false },
                new UserRole { Name = "Модератор", GlobalSettingsAllowed = false },
                new UserRole { Name = "Участник выставки", GlobalSettingsAllowed = false },
            };
            foreach (UserRole role in roles)
            {
                context.UserRoles.Add(role);
            }
            context.SaveChanges();
        }
    }
}
