using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VipCon.Data
{
    public static class MyIdentityDataInitializer
    {
        public static void SeedData(UserManager<ApplicationUser> userManager,RoleManager<MyIdentityRole> roleManager)
        {
            SeedRoles(roleManager);
            SeedUsers(userManager);
        }

        public static void SeedUsers(UserManager<ApplicationUser> userManager)
        {
            if (userManager.FindByNameAsync("normal@localhost").Result == null)
            {
                ApplicationUser user = new ApplicationUser();
                user.UserName = "normal@localhost";
                user.Email = "normal@localhost";                

                IdentityResult result = userManager.CreateAsync(user, "normal123").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user,"NormalUser").Wait();
                }
            }


            if (userManager.FindByNameAsync("admin@localhost").Result == null)
            {
                ApplicationUser user = new ApplicationUser();
                user.UserName = "admin@localhost";
                user.Email = "admin@localhost";
               

                IdentityResult result = userManager.CreateAsync(user, "admin123").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user,"Administrator").Wait();
                }
            }
        }

        public static void SeedRoles (RoleManager<MyIdentityRole> roleManager)
        {
            if (!roleManager.RoleExistsAsync("NormalUser").Result)
            {
                MyIdentityRole role = new MyIdentityRole();
                role.Name = "NormalUser";                
                IdentityResult roleResult = roleManager.
                CreateAsync(role).Result;
            }


            if (!roleManager.RoleExistsAsync("Administrator").Result)
            {
                MyIdentityRole role = new MyIdentityRole();
                role.Name = "Administrator";                
                IdentityResult roleResult = roleManager.
                CreateAsync(role).Result;
            }
        }
    }
}
