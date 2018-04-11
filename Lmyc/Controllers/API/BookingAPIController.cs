using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lmyc.Data;
using Lmyc.Models;
using Lmyc.Models.BookingViewModels;
using Lmyc.Models.UserViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lmyc.Controllers.API
{
    [Produces("application/json")]
    [Route("api/Bookingsapi")]
    public class BookingAPIController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;


        public BookingAPIController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: api/bookingApi
        [HttpGet]
        public IEnumerable<BookingViewModel> GetBooking()
        {
            var bookings = _context.Bookings
                .Include(b => b.Boat)
                .Include(b => b.User)
                .Include(b => b.UserBookings)
                    .ThenInclude(b => b.User)
                .OrderBy(b => b.StartDateTime);
            
            var bookingModels = new List<BookingViewModel>();
            
            foreach (var b in bookings)
            {
                var userRoles = new List<UserRoleData>();

                foreach (var br in b.UserBookings)
                {
                    var userRole = new UserRoleData
                    {
                        Name = br.User.FirstName + " " + br.User.LastName,
                        RoleName = string.Join(", ", _userManager.GetRolesAsync(b.User).Result)
                    };

                    userRoles.Add(userRole);
                }

                var booking = new BookingViewModel
                {
                    BookingId = b.BookingId,
                    BoatId = b.BoatId,
                    BoatName = b.Boat.BoatName,
                    StartDateTime = b.StartDateTime,
                    EndDateTime = b.EndDateTime,
                    NonMemberCrews = b.NonMemberCrews,
                    Itinerary = b.Itinerary,
                    AllocatedHours = b.AllocatedHours,
                    UserId = b.UserId,
                    FirstName = b.User.FirstName,
                    LastName = b.User.LastName,
                    MemberCrews = userRoles
                };

                bookingModels.Add(booking);
            }
            return bookingModels;
        }

        // POST: api/bookingsapi
        [HttpPost]
        public async Task<IActionResult> PostBooking([FromBody] Booking booking, string[] memberCrews)
        {
            if (memberCrews == null)
            {
                return BadRequest("No member crew is selected");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            booking.UserBookings = new List<UserBooking>();

            foreach (var member in memberCrews)
            {
                var BookingToAdd = new UserBooking
                {
                    BookingId = booking.BookingId,
                    UserId = member
                };

                booking.UserBookings.Add(BookingToAdd);
            }

            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBooking", new { id = booking.BookingId }, booking);
        }
    }

}