using Domain;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence
{
    public class Seed
    {
        public static async Task SeedAmin(DataContext context, UserManager<AppUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                var admin = new AppUser
                {
                    UserName = "admin",
                    Email = "admin@admin.com",
                };

                await userManager.CreateAsync(admin, "Pa$$w0rd");
            }

        }
    }
}
