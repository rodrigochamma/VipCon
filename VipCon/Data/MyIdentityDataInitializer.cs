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
            //if (userManager.FindByNameAsync("parceiro@gmail.com").Result == null)
            //{
            //    ApplicationUser user = new ApplicationUser();
            //    user.UserName = "parceiro@gmail.com";
            //    user.Email = "parceiro@gmail.com";
            //    user.Nome = "Parceiro";
            //    user.Admin = false;

            //    IdentityResult result = userManager.CreateAsync(user, "parceiro123").Result;

            //    if (result.Succeeded)
            //    {
            //        userManager.AddToRoleAsync(user, "Parceiro").Wait();
            //    }
            //}


            if (userManager.FindByNameAsync("paduacastrosti@gmail.com").Result == null)
            {
                ApplicationUser user = new ApplicationUser();
                user.UserName = "paduacastrosti@gmail.com";
                user.Email = "paduacastrosti@gmail.com";
                user.Nome = "PCS";
                user.Admin = true;

                IdentityResult result = userManager.CreateAsync(user, "pcs123").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user,"Administrator").Wait();
                }
            }
        }

        public static void SeedRoles (RoleManager<MyIdentityRole> roleManager)
        {
            if (!roleManager.RoleExistsAsync("Parceiro").Result)
            {
                MyIdentityRole role = new MyIdentityRole();
                role.Name = "Parceiro";                
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
