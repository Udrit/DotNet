using Microsoft.AspNetCore.Identity;
using UdritDhakal_PMS.Models;

namespace UdritDhakal_PMS.Data
{
    public class SeedingData
    {

        public static async Task InitializeAsync(IServiceProvider serviceProvider)
        {
            try
            {
                var _roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var _userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                String[] Roles = { "ADMIN", "USER" };
                foreach (String roleName in Roles)
                {
                    if (!await _roleManager.RoleExistsAsync(roleName))
                    {
                        await _roleManager.CreateAsync(new IdentityRole(roleName));
                    }
                }

                if (await _userManager.FindByNameAsync("c") == null)
                {
                    var role = _roleManager.FindByNameAsync("ADMIN").Result;

                    var user = new ApplicationUser()
                    {

                        FirstName = "Product",
                        LastName = "Admin",
                        IsActive = true,
                        UserRoleId = role.Id,
                        UserName = "admin@admin.com",
                        Email = "admin@admin.com",
                        PhoneNumber = "9231472534",
                        Address = "Kathmandu",
                        CreatedBy = "admin",
                        CreatedDate = DateTime.Now
                    };

                    var res = await _userManager.CreateAsync(user, "Admin`123");
                    await _userManager.SetLockoutEnabledAsync(user, false);
                    if (res.Succeeded)
                    {
                        await _userManager.AddToRoleAsync(user, "ADMIN");
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
