using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace DeliverySystem.Infrastructure.Persistence.Identity;

public class DataService
{

    public static async Task DataSeed(IServiceProvider serviceProvider)
    {
        var scope = serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        var userManager = scope.ServiceProvider.GetService<UserManager<ApplicationUser>>();
        var roleManager = scope.ServiceProvider.GetService<RoleManager<IdentityRole>>();
        var optionConfigure = scope.ServiceProvider.GetService<IOptions<AdminInfoOption>>();
        var logger = scope.ServiceProvider.GetService<ILogger<DataService>>();

        try
        {
            logger!.LogInformation("Ensure Data Base is Exist And Any PendingMigrations");
            if (context.Database.GetPendingMigrations().Any())
                context.Database.Migrate();

            logger!.LogInformation("Seeding application roles...");
            List<string> roles = ["Admin", "Customer", "DeliveryAgent", "Merchant"];
            foreach (var item in roles)
                await AddRoleAsync(roleManager!, item);

            var email = optionConfigure!.Value.Email;
            if (await userManager!.FindByEmailAsync(email) is null)
            {
                var user = new ApplicationUser
                {
                    UserName = optionConfigure.Value.Name,
                    FullName = optionConfigure.Value.Name,
                    Email = email
                };
                var result = await userManager.CreateAsync(user, optionConfigure.Value.Password);
                if (!result.Succeeded)
                {
                    logger!.LogError("Create User Admin is failed");
                    throw new Exception($"Create User Admin is failed Error is :{string.Join(" , ", result.Errors.Select(e => e.Description))}");
                }
                else
                {
                    logger!.LogInformation("Add Admin User Successfully");
                    var roleResult = await userManager.AddToRoleAsync(user, "Admin");
                    if (!roleResult.Succeeded)
                    {
                        logger!.LogError($"Delete User {string.Join(" , ", roleResult.Errors.Select(e => e.Description))}");
                        await userManager.DeleteAsync(user);
                    }
                }
            }


        }
        catch (Exception ex)
        {
            logger!.LogError($"Error Accrued in DataBase message{ex.Message}");
        }
    }
    public async static Task AddRoleAsync(RoleManager<IdentityRole> roleManager, string roleName)
    {
        if (!await roleManager.RoleExistsAsync(roleName))
        {
            var result = await roleManager.CreateAsync(new IdentityRole(roleName));

            if (!result.Succeeded)
            {
                throw new Exception($"failed to create role {roleName} ,Error {string.Join(" ", result.Errors.Select(e => e.Description))} ");
            }
        }
    }

}
