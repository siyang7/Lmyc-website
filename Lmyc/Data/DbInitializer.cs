using Lmyc.Models;
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
                string[] roleNames = 
                    { Role.Admin, Role.MemberGoodStanding, Role.MemberNotGoodStanding, Role.AssociateMember, Role.BookingModerator, Role.Crew, Role.DaySkipper, Role.CruiseSkipper };

                foreach (var roleName in roleNames)
                {
                    await EnsureRole(serviceProvider, null, roleName);
                }

                var admin = new ApplicationUser
                {
                    UserName = "admin@a.com",
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
                    SailingExperience = 50,
                    StartingCredit = 320,
                    CreditBalance = 320
                };


                var adminId = await EnsureUser(serviceProvider, admin, "P@$$w0rd");
                await EnsureRole(serviceProvider, adminId, Role.Admin);
                await EnsureRole(serviceProvider, adminId, Role.CruiseSkipper);
                await EnsureRole(serviceProvider, adminId, Role.DaySkipper);
                await EnsureRole(serviceProvider, adminId, Role.MemberGoodStanding);

                var users = GetApplicationUsers();
                foreach (var user in users)
                {
                    var userId = await EnsureUser(serviceProvider, user, "P@$$w0rd");
                    await EnsureRole(serviceProvider, userId, Role.MemberGoodStanding);
                    await EnsureRole(serviceProvider, userId, Role.CruiseSkipper);
                    await EnsureRole(serviceProvider, userId, Role.DaySkipper);
                }

                // Look for any boats in the database
                if (context.Boats.Any())
                {
                    return; // DB have been seeded
                }

                var boats = GetBoats();
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
                    StartingCredit = newUser.StartingCredit,
                    CreditBalance = newUser.CreditBalance,
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

        private static List<ApplicationUser> GetApplicationUsers()
        {
            List<ApplicationUser> users = new List<ApplicationUser>
            {
                new ApplicationUser
                {
                    UserName = "castiel@bcit.ca",
                    Email = "castiel@bcit.ca",
                    FirstName = "Castiel",
                    LastName = "Li",
                    Street = "3700 Willingdon Ave",
                    City = "Burnaby",
                    Province = "BC",
                    PostalCode = "V5G 3H2",
                    Country = "Canada",
                    HomePhone = "(604) 434-5734",
                    EmergencyContactOne = "(604) 434-5734",
                    Skills = "none",
                    SailingQualifications = "none",
                    SailingExperience = 2,
                    StartingCredit = 320,
                    CreditBalance = 320
                },
                new ApplicationUser
                {
                    UserName = "bryan@bcit.ca",
                    Email = "bryan@bcit.ca",
                    FirstName = "Bryan",
                    LastName = "Brotonel",
                    Street = "3700 Willingdon Ave",
                    City = "Burnaby",
                    Province = "BC",
                    PostalCode = "V5G 3H2",
                    Country = "Canada",
                    HomePhone = "(604) 434-5734",
                    EmergencyContactOne = "(604) 434-5734",
                    Skills = "none",
                    SailingQualifications = "none",
                    SailingExperience = 1,
                    StartingCredit = 320,
                    CreditBalance = 320
                },
                new ApplicationUser
                {
                    UserName = "nate@bcit.ca",
                    Email = "nate@bcit.ca",
                    FirstName = "Nate",
                    LastName = "Chiang",
                    Street = "3700 Willingdon Ave",
                    City = "Burnaby",
                    Province = "BC",
                    PostalCode = "V5G 3H2",
                    Country = "Canada",
                    HomePhone = "(604) 434-5734",
                    EmergencyContactOne = "(604) 434-5734",
                    Skills = "none",
                    SailingQualifications = "none",
                    SailingExperience = 5,
                    StartingCredit = 320,
                    CreditBalance = 320
                },
                new ApplicationUser
                {
                    UserName = "jason@bcit.ca",
                    Email = "jason@bcit.ca",
                    FirstName = "Jason",
                    LastName = "Chen",
                    Street = "3700 Willingdon Ave",
                    City = "Burnaby",
                    Province = "BC",
                    PostalCode = "V5G 3H2",
                    Country = "Canada",
                    HomePhone = "(604) 434-5734",
                    EmergencyContactOne = "(604) 434-5734",
                    Skills = "none",
                    SailingQualifications = "none",
                    SailingExperience = 3,
                    StartingCredit = 320,
                    CreditBalance = 320
                }
            };

            return users;
        }

        private static List<Boat> GetBoats()
        {
            List<Boat> boats = new List<Boat>()
            {
                new Boat()
                {
                    BoatName = "Sharqui",
                    BoatStatus = BoatStatus.Operational,
                    BoatPicture = new Byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 },
                    BoatDescription = "Sharqui was added to the fleet in 2016.  Another of the very popular C&C designs for style, " +
                                       "comfort, and speed. Sharqui sleeps five comfortably, has an aftermarket outboard motor, and sports" +
                                       "a very generous dodger for protection on heavy weather days.",
                    BoatLength = 27,
                    BoatMake = "C&C",
                    BoatYear = 1981,
                    CreditsPerHourOfUsage = 6
                },
                new Boat()
                {
                    BoatName = "Pegasus",
                    BoatStatus = BoatStatus.Operational,
                    BoatPicture = new Byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 },
                    BoatDescription = "Pegasus will be oufitted for travelling to Desolation Sound for the first time this summer. Members are " +
                    "looking forward to a roomier more comfortable boat with generous side decks.",
                    BoatLength = 27,
                    BoatMake = "C&C",
                    BoatYear = 1979,
                    CreditsPerHourOfUsage = 6
                },
                new Boat()
                {
                    BoatName = "Lightcure",
                    BoatStatus = BoatStatus.Operational,
                    BoatPicture = new Byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 },
                    BoatDescription = "She is one of our most popular boats, being a good sailor and comfortable while cruising. She sleeps " +
                    "5 adults comfortably.She was refitted in 2005 and is powered by a remote controlled Yamaha outboard." +
                    " Lightcure has a BBQ, cockpit table, asymmetrical spinnaker and all the extras to be comfortable for" +
                    " cruising.She is also rigged for use in local sailboat races.",
                    BoatLength = 27,
                    BoatMake = "C&C Mark 3",
                    BoatYear = 1979,
                    CreditsPerHourOfUsage = 6
                },
                new Boat()
                {
                    BoatName = "Frankie",
                    BoatStatus = BoatStatus.Operational,
                    BoatPicture = new Byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 },
                    BoatDescription = "She is designated as a “day sailor”, and is available for use in Semiahmoo Bay. " +
                    "She is outfitted with some of the amenities for cruising and may be used occasionally for overnight trips." +
                    "She might sleep 4 adults comfortably.Frankie has a spray dodger and is powered by a Yamaha outboard.`",
                    BoatLength = 25,
                    BoatMake = "'Cal Mark 2'",
                    BoatYear = 1983,
                    CreditsPerHourOfUsage = 6
                },
                new Boat()
                {
                    BoatName = "White Swan",
                    BoatStatus = BoatStatus.Operational,
                    BoatPicture = new Byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 },
                    BoatDescription = "She is a cruising boat, with a spray dodger, inboard diesel engine and enclosed head." +
                    " White Swan is popular for longer trips to the local islands.She sleeps 4 adults very" +
                    " comfortably with a private aft cabin and V - berth",
                    BoatLength = 28,
                    BoatMake = "MkII",
                    BoatYear = 1979,
                    CreditsPerHourOfUsage = 6
                },
                new Boat()
                {
                    BoatName = "Peak Time",
                    BoatStatus = BoatStatus.Operational,
                    BoatPicture = new Byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 },
                    BoatDescription = "She has a spray dodger, BBQ and a comfortable cockpit." +
                    " She has all the amenities and can be used as a cruiser or day sailing boat." +
                    " She can sleep 4 adults.Peak Time is powered by a Yamaha outboard engine." +
                    " She is also rigged for use in local sailboat races",
                    BoatLength = 27,
                    BoatMake = "C&C Mark 5",
                    BoatYear = 1985,
                    CreditsPerHourOfUsage = 6
                },
                new Boat()
                {
                    BoatName = "Pegasus",
                    BoatStatus = BoatStatus.Operational,
                    BoatPicture = new Byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 },
                    BoatDescription = "A spacious fast cruiser She has a comfortable cockpit, spray dodger." +
                    " She has all the amenities of a cruiser Large aft head/shower" +
                    " She can sleep up to 6 adults in comfort Powered by a Yanmar diesel." +
                    " Stable wing keel design Open transom with swim grid, BBQ for sailing adventures" +
                    " She is a cruising boat, with a spray dodger, inboard diesel engine and enclosed head." +
                    " White Swan is popular for longer trips to the local islands.She sleeps 4 adults very" +
                    " comfortably with a private aft cabin and V-berth",
                    BoatLength = 30,
                    BoatMake = "Cruiser",
                    BoatYear = 1979,
                    CreditsPerHourOfUsage = 6
                }
            };

            return boats;
        }

        //private static List<Booking> GetBookings(ApplicationDbContext context)
        //{
        //    List<Booking> bookings = new List<Booking>
        //    {
        //        new Booking
        //        {
        //            StartDateTime = DateTime.Now.AddDays(7),
        //            StartDateTime = DateTime.Now.AddDays(8),
        //            Boat = context.Boats.FirstOrDefault(b => b.BoatId == 1)
                    
        //        }
        //    };
        //}
    }
}
