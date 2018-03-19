using System;
using Lmyc.Data;
using Lmyc.Models;
using Microsoft.EntityFrameworkCore;

namespace Lmyc.Tests
{
    public class MockDbContext
    {
        public MockDbContext()
        {
        }

        private ApplicationDbContext GetContextWithData()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                              .UseInMemoryDatabase(Guid.NewGuid().ToString())
                              .Options;
            var context = new ApplicationDbContext(options);

            var boat = new Boat { BoatId = "hexstring", BoatName = "Beers" };

            context.Boats.Add(boat);

            context.SaveChanges();

            return context;
        }
    }
}
