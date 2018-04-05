using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Lmyc.Data;
using Lmyc.Models;
using Microsoft.AspNetCore.Identity;

namespace Lmyc.Controllers.MVC
{
    public class ReservationsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ReservationsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Reservations
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var applicationDbContext = _context.Reservations.Include(r => r.Boat);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Reservations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservations
                .Include(r => r.Boat)
                .Include(r => r.User)
                .SingleOrDefaultAsync(m => m.ReservationId == id);
            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }

        // GET: Reservations/Create
        public IActionResult Create()
        {
            ViewData["ReservedBoat"] = new SelectList(_context.Boats, "BoatName", "BoatName");
            ViewData["MemberCrew"] = new SelectList(_context.Users, "Id", "Username");
            return View();
        }

        // POST: Reservations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ReservationId,ReservedBoat,StartDateTime,EndDateTime,Itinerary,AllocatedCredit,AllocatedHours")] Reservation reservation)
        {
            // Trying to make it valid.. and failing
            //var user = await _userManager.GetUserAsync(User);
            //reservation.User = user ?? throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            //reservation.Boat = await _context.Boats.FirstAsync(b => b.BoatName == reservation.ReservedBoat);
            //reservation.CreatedBy = user.UserName;
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                reservation.User = user ?? throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
                _context.Add(reservation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ReservedBoat"] = new SelectList(_context.Boats, "BoatName", "BoatName", reservation.ReservedBoat);
            return View(reservation);
        }

        // GET: Reservations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservations
                .Include(r => r.Boat)
                .Include(r => r.User)
                .SingleOrDefaultAsync(m => m.ReservationId == id);
            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }

        // POST: Reservations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reservation = await _context.Reservations.SingleOrDefaultAsync(m => m.ReservationId == id);
            _context.Reservations.Remove(reservation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReservationExists(int id)
        {
            return _context.Reservations.Any(e => e.ReservationId == id);
        }
    }
}
