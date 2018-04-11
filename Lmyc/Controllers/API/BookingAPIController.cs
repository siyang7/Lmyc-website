using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lmyc.Data;
using Lmyc.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lmyc.Controllers.API
{
    [Produces("application/json")]
    [Route("api/BookingAPI")]
    public class BookingAPIController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BookingAPIController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/bookingApi
        [HttpGet]
        public IEnumerable<Booking> GetBooking()
        {
            return _context.Bookings.Include(b => b.Boat).Include(b => b.User);
        }
    }

}