using E_Commerce.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Commerce.Model;
using E_Commerce.DAL.Model;
using Microsoft.AspNetCore.Identity;

namespace E_Commerce.DAL.Utils
{
    public class SeedData : ISeedData
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public SeedData(
            ApplicationDbContext dbContext,
            RoleManager<IdentityRole> roleManager,
            UserManager<ApplicationUser> userManager
            )
        {
            _roleManager = roleManager;
            _dbContext = dbContext;
            _userManager = userManager;
        }
        public async Task DataSeedingAsync()
        {
            
            if((await _dbContext.Database.GetPendingMigrationsAsync()).Any()){
               await  _dbContext.Database.MigrateAsync();
            }
            if (!await _dbContext.Categories.AnyAsync())
            {
                await _dbContext.Categories.AddRangeAsync(
                    new Category { Name = "Clothes" },
                    new Category { Name = "Phones" }
                    );
            }
            if (!await _dbContext.Brands.AnyAsync())
            {
                await _dbContext.Brands.AddRangeAsync(
                    new Brand { Name = "Iphone" },
                    new Brand { Name = "Samsong" }
                    );
            }
            await _dbContext.SaveChangesAsync();
        }

        public async Task IdentityDataSeedingAsync()
        {
            if (!await _roleManager.Roles.AnyAsync())
            {
                await _roleManager.CreateAsync(
                    new IdentityRole("Admin")
                );
                await _roleManager.CreateAsync(
                   new IdentityRole("SuperAdmin")
               ); await _roleManager.CreateAsync(
                    new IdentityRole("Customer")
                );
            }
            if(!await _userManager.Users.AnyAsync())
            {
                var user1 = new ApplicationUser()
                {
                    Email = "tojan050@gmail.com",
                    FullName = "Tojan Abu Gholah",
                    UserName = "tojan",
                    EmailConfirmed = true

                };
                var user2 = new ApplicationUser()
                {
                    Email = "arwa050@gmail.com",
                    FullName = "Arwa Abu Gholah",
                    UserName = "arwa",
                    EmailConfirmed = true

                };
                await _userManager.CreateAsync(user1);
                await _userManager.CreateAsync(user2);
     
                await _userManager.AddPasswordAsync(user1, "123soso@S");
                await _userManager.AddPasswordAsync(user2, "123soso@S");

              

                await _userManager.AddToRoleAsync(user1, "Admin");
                await _userManager.AddToRoleAsync(user1, "SuperAdmin");

            }
            await _dbContext.SaveChangesAsync();
        }
    }
}
