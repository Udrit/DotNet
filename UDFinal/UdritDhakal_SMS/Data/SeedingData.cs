using Microsoft.AspNetCore.Identity;

namespace UdritDhakal_SMS.Data
{
    public class SeedingData
    {
        public static async Task InitializeAsync(IServiceProvider serviceProvider)
        {
            var _userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var _roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            string[] roles = { "SUPERADMIN", "ADMIN", "STUDENT", "USER" };
            foreach (string role in roles)
            {
                if (!await _roleManager.RoleExistsAsync(role))
                {
                    await _roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            if (await _userManager.FindByNameAsync("superadmin@gmail.com") == null)
            {
                var role = _roleManager.FindByNameAsync("SUPERADMIN").Result;
                var password = "Superadmin@123";

                var user = new ApplicationUser()
                {
                    UserName = "superadmin@gmail.com",
                    Address = "Kopundole",
                    FirstName = "super",
                    LastName = "admin",
                    PhoneNumber = "9860440627",
                    UserRoleId = role.Id,
                    Email = "superadmin@gmail.com",
                    IsActive = true,
                    CreatedBy = "superadmin",
                    CreatedDate = DateTime.Now
                };

                await _userManager.SetPhoneNumberAsync(user, user.PhoneNumber);
                var result = await _userManager.CreateAsync(user, password);
                await _userManager.SetLockoutEnabledAsync(user, false);


                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "SUPERADMIN");
                }

            }
        }
    }
}
