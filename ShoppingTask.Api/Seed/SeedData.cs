using Microsoft.AspNetCore.Identity;
using ShoppingTask.Core.Models;
using ShoppingTask.Infrastructure.Data;

namespace ShoppingTask.API.Seed
{
    public class SeedData
    {
        public static async Task Seed(IApplicationBuilder app)
        {
            var services = app.ApplicationServices.CreateScope().ServiceProvider;
            var context = services.GetService<AppDbContext>()!;
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole<Guid>>>();
            var userManager = services.GetRequiredService<UserManager<User>>();

            await SeedRoles(context, roleManager);
        }

        private static async Task SeedRoles(
            AppDbContext context,
            RoleManager<IdentityRole<Guid>> roleManager
        )
        {
            if (roleManager.Roles.Any())
                return;

            var roles = Enum.GetValues(typeof(Roles)).Cast<Roles>().Select(a => a.ToString());

            foreach (var Role in roles)
            {
                await roleManager.CreateAsync(
                    new IdentityRole<Guid> { Id = Guid.NewGuid(), Name = Role }
                );
            }
            await context.SaveChangesAsync();
        }
    }
}
