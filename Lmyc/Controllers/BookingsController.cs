using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Lmyc.Data;
using Lmyc.Models;
using Lmyc.Models.BookingViewModels;
using Microsoft.AspNetCore.Identity;
using Lmyc.Models.UserViewModels;
using Microsoft.AspNetCore.Authorization;

namespace Lmyc.Controllers
{
    [Authorize(Policy="RequireLogin")]
    public class BookingsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public BookingsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Bookings
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Bookings.Include(b => b.Boat).Include(b => b.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Bookings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var booking = await _context.Bookings
                .Include(b => b.Boat)
                .Include(b => b.User)
                .Include(b => b.UserBookings)
                    .ThenInclude(b => b.User)
                .OrderBy(b => b.StartDateTime)
                .SingleOrDefaultAsync(m => m.BookingId == id);

            if (booking == null)
            {
                return NotFound();
            }

            var userRoles = new List<UserRoleData>();

            foreach (var b in booking.UserBookings)
            {
                var userRole = new UserRoleData
                {
                    FisrtName = b.User.FirstName,
                    LastName = b.User.LastName,
                    RoleName = string.Join(",", _userManager.GetRolesAsync(b.User).Result)
                };

                userRoles.Add(userRole);
            }

            var model = new BookingViewModel
            {
                BookingId = booking.BookingId,
                BoatId = booking.BoatId,
                BoatName = booking.Boat.BoatName,
                StartDateTime = booking.StartDateTime,
                EndDateTime = booking.EndDateTime,
                NonMemberCrews = booking.NonMemberCrews,
                Itinerary = booking.Itinerary,
                AllocatedHours = booking.AllocatedHours,
                UserId = booking.UserId,
                MemberCrews = userRoles
            };

            return View(model);
        }

        // GET: Bookings/Create
        public IActionResult Create()
        {
            var booking = new Booking
            {
                UserBookings = new List<UserBooking>()
            };
            PopulateBookingUserData(booking);
            ViewData["Boats"] = new SelectList(_context.Boats, "BoatId", "BoatName");
            return View();
        }

        // POST: Bookings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BoatId,StartDateTime,EndDateTime,AllocatedHours,NonMemberCrews,Itinerary")]Booking booking, string[] memberCrews)
        {
            if (memberCrews != null)
            {
                booking.UserBookings = new List<UserBooking>();
            }
            
            foreach (var user in memberCrews)
            {
                var BookingToAdd = new UserBooking
                {
                    BookingId = booking.BookingId,
                    UserId = user
                };

                booking.UserBookings.Add(BookingToAdd);
            }

            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                booking.UserId = user.Id;
                _context.Add(booking);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            PopulateBookingUserData(booking);
            ViewData["Boats"] = new SelectList(_context.Boats, "BoatId", "BoatName", booking.BoatId);
            return View(booking);
        }

        // GET: Bookings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var booking = await _context.Bookings.SingleOrDefaultAsync(m => m.BookingId == id);
            if (booking == null)
            {
                return NotFound();
            }

            ViewData["BoatId"] = new SelectList(_context.Boats, "BoatId", "BoatName", booking.BoatId);
            return View(booking);
        }

        // POST: Bookings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BookingId,StartDateTime,EndDateTime,NonMemberCrew,Itinerary,AllocatedHours,UserId,BoatId")] Booking booking)
        {
            if (id != booking.BookingId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(booking);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookingExists(booking.BookingId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["BoatId"] = new SelectList(_context.Boats, "BoatId", "BoatName", booking.BoatId);
            return View(booking);
        }

        // GET: Bookings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var booking = await _context.Bookings
                .Include(b => b.Boat)
                .Include(b => b.User)
                .SingleOrDefaultAsync(m => m.BookingId == id);
            if (booking == null)
            {
                return NotFound();
            }

            return View(booking);
        }

        // POST: Bookings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var booking = await _context.Bookings.SingleOrDefaultAsync(m => m.BookingId == id);
            _context.Bookings.Remove(booking);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookingExists(int id)
        {
            return _context.Bookings.Any(e => e.BookingId == id);
        }

        private void PopulateBookingUserData(Booking booking)
        {
            var users = _context.Users;
            var userBookings = new HashSet<string>(booking.UserBookings.Select(u => u.UserId));
            var model = new List<BookingUserData>();
            
            foreach (var user in users)
            {
                model.Add(new BookingUserData
                {
                    UserId = user.Id,
                    Role = string.Join(", ", _userManager.GetRolesAsync(user).Result),
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Assigned = userBookings.Contains(user.Id)
                });
            }

            ViewData["Users"] = model;
        }
    }
}
