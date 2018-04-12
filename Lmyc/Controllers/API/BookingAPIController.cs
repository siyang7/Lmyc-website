﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lmyc.Data;
using Lmyc.Helper;
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

        // api/bookingapi/GetAllocatedHours
        [HttpPost]
        [Route("GetAllocatedHours")]
        public int GetAllocatedHours(string start, string end)
        {
            DateTime startDate = Convert.ToDateTime(start);
            DateTime endDate = Convert.ToDateTime(end);
            return AllocatedHoursCalculator.CalculateAllocatedHours(startDate, endDate);
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
                        FisrtName = br.User.FirstName,
                        LastName = br.User.LastName,
                        RoleName = string.Join(",", _userManager.GetRolesAsync(b.User).Result)
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
        public async Task<IActionResult> PostBooking([FromBody] BookingViewModel bookingModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var errors = BookingRules(bookingModel).Result;

            if (!string.IsNullOrEmpty(errors))
            {
                return BadRequest(errors);
            }

            var allocatedHours = AllocatedHoursCalculator.CalculateAllocatedHours(bookingModel.StartDateTime, bookingModel.EndDateTime);

            var booking = new Booking
            {
                BoatId = bookingModel.BoatId,
                StartDateTime = bookingModel.StartDateTime,
                EndDateTime = bookingModel.EndDateTime,
                NonMemberCrews = bookingModel.NonMemberCrews,
                Itinerary = bookingModel.NonMemberCrews,
                AllocatedHours = allocatedHours,
                UserId = bookingModel.UserId
            };

            booking.UserBookings = new List<UserBooking>();

            foreach (var member in bookingModel.MemberCrews)
            {
                var user = await _userManager.FindByIdAsync(member.UserId);

                user.CreditBalance -= member.UsedCredit;

                _context.Entry(user).State = EntityState.Modified;

                await _context.SaveChangesAsync();

                var BookingToAdd = new UserBooking
                {
                    BookingId = booking.BookingId,
                    UserId = member.UserId,
                    UsedCredit = member.UsedCredit
                };

                booking.UserBookings.Add(BookingToAdd);
            }

            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();

            return Ok(bookingModel);
        }

        // DELETE: api/bookingsapi/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBooking([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var booking = await _context.Bookings
                .Include(b => b.UserBookings)
                    .ThenInclude(b => b.User)
                .SingleOrDefaultAsync(b => b.BookingId == id);

            if (booking == null)
            {
                return NotFound();
            }

            foreach (var b in booking.UserBookings)
            {
                // return the credit
                b.User.CreditBalance += b.UsedCredit;
            }

            _context.Bookings.Remove(booking);
            await _context.SaveChangesAsync();

            return Ok();

        }

        private async Task<string> BookingRules(BookingViewModel bookingModel)
        {
            // crew requirements
            bool daySkipper = false;
            bool cruiseSkipper = false;
            int totalCredit = 0;

            var bookings = _context.Bookings.Where(b => b.BoatId == bookingModel.BoatId);

            foreach (var b in bookings)
            {
                if (bookingModel.StartDateTime < b.EndDateTime && b.StartDateTime < bookingModel.EndDateTime)
                {
                    return "Overlap booking with " + bookingModel.BoatName;
                }
            }

            var user = await _userManager.FindByIdAsync(bookingModel.UserId);

            if (user == null)
            {
                return "User not found";
            }

            if (!await _userManager.IsInRoleAsync(user, Role.MemberGoodStanding))
            {
                return "This user cannot make booking";
            }

            if (bookingModel.EndDateTime.Subtract(bookingModel.StartDateTime).Duration().TotalHours > 72
                || bookingModel.EndDateTime.Subtract(bookingModel.StartDateTime).Duration().TotalHours <= 0)
            {
                return "Booking only allows less than 72 hours";
            }

            if (bookingModel.MemberCrews == null)
            {
                return "No member crew is selected";
            }

            foreach (var m in bookingModel.MemberCrews)
            {

                var member = await _userManager.FindByIdAsync(m.UserId);

                if (member.CreditBalance < m.UsedCredit)
                {
                    return member.FirstName + " " + member.LastName + " has no enough credits";
                }

                totalCredit += m.UsedCredit;

                if (_userManager.IsInRoleAsync(member, Role.CruiseSkipper).Result)
                {
                    cruiseSkipper = true;
                }

                if (_userManager.IsInRoleAsync(member, Role.DaySkipper).Result)
                {
                    daySkipper = true;
                }
            }

            var days = bookingModel.EndDateTime.Subtract(bookingModel.StartDateTime).Duration().TotalDays;

            if (days >= 1 && !cruiseSkipper)
            {
                return "Requires Cruise Skipper";
            }
            else
            {
                if (days < 1 && !(daySkipper || cruiseSkipper))
                {
                    return "Requires Day Skippers";
                }
            }

            var boat = await _context.Boats.SingleOrDefaultAsync(b => b.BoatId == bookingModel.BoatId);

            if (boat == null)
            {
                return "Boat Not Found";
            }

            var allocatedHours = AllocatedHoursCalculator.CalculateAllocatedHours(bookingModel.StartDateTime, bookingModel.EndDateTime);

            if (totalCredit != allocatedHours * boat.CreditsPerHourOfUsage)
            {
                return "Total Credit doesnt match " + allocatedHours;
            }

            return string.Empty;
        }
    }
}