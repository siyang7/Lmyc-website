using Lmyc.Models;
using LMYCWebsite.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebSockets.Internal;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lmyc.Data
{
    public static class DbInitializer
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                string[] roleNames = { "Admin", "Member", "Associate Member", "Booking Moderator" };

                foreach (var roleName in roleNames)
                {
                    await EnsureRole(serviceProvider, null, roleName);
                }

                var admin = new ApplicationUser
                {
                    Email = "admin@a.com",
                    FirstName = "BCIT",
                    LastName = "COMP4976",
                    Street = "3700 Willingdon Ave",
                    City = "Burnaby",
                    Province = "BC",
                    PostalCode = "V5G 3H2",
                    Country = "Canada",
                    HomePhone = "(604) 434-5734",
                    EmergencyContactOne = "(604) 434-5734",
                    Skills = "none",
                    SailingQualifications = "none",
                    MemberStatus = MemberStatus.Approved,
                    SailingExperience = 50,
                    StartingCredit = 1000
                };


                var adminId = await EnsureUser(serviceProvider, admin, "P@$$w0rd");
                await EnsureRole(serviceProvider, adminId, "Admin");

                // Look for any boats in the database
                if (context.Boats.Any())
                {
                    return; // DB have been seeded
                }

                var boats = BoatDummyData.getBoats();
                foreach (Boat b in boats)
                {
                    context.Boats.Add(b);
                }

                context.SaveChanges();
            }
                
        }

        private static async Task<string> EnsureUser(IServiceProvider serviceProvider, ApplicationUser newUser, string password)
        {
            var userManager = serviceProvider.GetService<UserManager<ApplicationUser>>();
            var user = await userManager.FindByNameAsync(newUser.UserName);

            if (user == null)
            {
                user = new ApplicationUser
                {
                    FirstName = newUser.FirstName,
                    LastName = newUser.LastName,
                    HomePhone = newUser.HomePhone,
                    UserName = newUser.Email,
                    Email = newUser.Email,
                    Street = newUser.Street,
                    Province = newUser.Province,
                    City = newUser.City,
                    Country = newUser.Country,
                    PostalCode = newUser.PostalCode,
                    SailingExperience = newUser.SailingExperience,
                    Skills = newUser.Skills,
                    SailingQualifications = newUser.SailingQualifications,
                    EmergencyContactOne = newUser.EmergencyContactOne
                };

                await userManager.CreateAsync(user, password);
            }

            return user.Id;
        }

        private static async Task<IdentityResult> EnsureRole(IServiceProvider serviceProvider, string uid, string role)
        {
            IdentityResult identityResult = null;
            var roleManager = serviceProvider.GetService<RoleManager<IdentityRole>>();

            if (!await roleManager.RoleExistsAsync(role))
            {
                identityResult = await roleManager.CreateAsync(new IdentityRole(role));
            }

            if (uid != null)
            {
                var userManager = serviceProvider.GetService<UserManager<ApplicationUser>>();

                var user = await userManager.FindByIdAsync(uid);

                identityResult = await userManager.AddToRoleAsync(user, role);
            }

            return identityResult;
        }
    }
}
