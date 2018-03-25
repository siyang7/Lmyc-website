using Lmyc.Data;
using Lmyc.Models;
using LMYCWebsite.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lmyc.InitData
{
    public static class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            //Look for any boats in the database
            if(context.Boats.Any())
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
}
